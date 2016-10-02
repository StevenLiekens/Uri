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
            AlphaLexerFactory = alphaLexerFactory.Singleton();
            DigitLexerFactory = digitLexerFactory.Singleton();
        }

        public static UnreservedLexerFactory Default { get; } =
            new UnreservedLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.Core.ALPHA.AlphaLexerFactory.Default,
                Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default);

        public ILexerFactory<Alpha> AlphaLexerFactory { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public ILexerFactory<Digit> DigitLexerFactory { get; }

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
