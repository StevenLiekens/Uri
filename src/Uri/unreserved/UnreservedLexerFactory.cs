using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace UriSyntax.unreserved
{
    public class UnreservedLexerFactory : RuleLexerFactory<Unreserved>
    {
        static UnreservedLexerFactory()
        {
            Default = new UnreservedLexerFactory(
                Txt.ABNF.Core.ALPHA.AlphaLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public UnreservedLexerFactory(
            [NotNull] ILexerFactory<Alpha> alphaLexerFactory,
            [NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alphaLexerFactory));
            }
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            AlphaLexerFactory = alphaLexerFactory;
            DigitLexerFactory = digitLexerFactory;
        }

        [NotNull]
        public static UnreservedLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Alpha> AlphaLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; }

        public override ILexer<Unreserved> Create()
        {
            var innerLexer = Alternation.Create(
                AlphaLexerFactory.Create(),
                DigitLexerFactory.Create(),
                Terminal.Create(@"-", StringComparer.Ordinal),
                Terminal.Create(@".", StringComparer.Ordinal),
                Terminal.Create(@"_", StringComparer.Ordinal),
                Terminal.Create(@"~", StringComparer.Ordinal));
            return new UnreservedLexer(innerLexer);
        }
    }
}
