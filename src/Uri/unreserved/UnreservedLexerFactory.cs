using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace UriSyntax.unreserved
{
    public class UnreservedLexerFactory : LexerFactory<Unreserved>
    {
        static UnreservedLexerFactory()
        {
            Default = new UnreservedLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.Core.ALPHA.AlphaLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public UnreservedLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexerFactory<Alpha> alphaLexerFactory,
            [NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alphaLexerFactory));
            }
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            AlphaLexerFactory = alphaLexerFactory;
            DigitLexerFactory = digitLexerFactory;
        }

        [NotNull]
        public static UnreservedLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Alpha> AlphaLexerFactory { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<Unreserved> Create()
        {
            var innerLexer = AlternationLexerFactory.Create(
                AlphaLexerFactory.Create(),
                DigitLexerFactory.Create(),
                TerminalLexerFactory.Create(@"-", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@".", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"_", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"~", StringComparer.Ordinal));
            return new UnreservedLexer(innerLexer);
        }
    }
}
