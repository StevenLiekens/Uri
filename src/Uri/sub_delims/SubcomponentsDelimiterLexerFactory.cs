using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.sub_delims
{
    public class SubcomponentsDelimiterLexerFactory : LexerFactory<SubcomponentsDelimiter>
    {
        static SubcomponentsDelimiterLexerFactory()
        {
            Default = new SubcomponentsDelimiterLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default);
        }

        public SubcomponentsDelimiterLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
        }

        [NotNull]
        public static SubcomponentsDelimiterLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<SubcomponentsDelimiter> Create()
        {
            // "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
            var innerLexer = AlternationLexerFactory.Create(
                TerminalLexerFactory.Create(@"!", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"$", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"&", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"'", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"(", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@")", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"*", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"+", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@",", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@";", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"=", StringComparer.Ordinal));

            // sub-delims
            return new SubcomponentsDelimiterLexer(innerLexer);
        }
    }
}
