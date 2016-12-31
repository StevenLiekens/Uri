using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace UriSyntax.scheme
{
    public class SchemeLexerFactory : RuleLexerFactory<Scheme>
    {
        static SchemeLexerFactory()
        {
            Default = new SchemeLexerFactory(
                Txt.ABNF.Core.ALPHA.AlphaLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public SchemeLexerFactory(
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
        public static SchemeLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Alpha> AlphaLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; }

        public override ILexer<Scheme> Create()
        {
            var alpha = AlphaLexerFactory.Create();
            var digit = DigitLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                alpha,
                Repetition.Create(
                    Alternation.Create(
                        alpha,
                        digit,
                        Terminal.Create(@"+", StringComparer.Ordinal),
                        Terminal.Create(@"-", StringComparer.Ordinal),
                        Terminal.Create(@".", StringComparer.Ordinal)),
                    0,
                    int.MaxValue));
            return new SchemeLexer(innerLexer);
        }
    }
}
