using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;

namespace Uri.sub_delims
{
    public class SubcomponentsDelimiterLexerFactory : ILexerFactory<SubcomponentsDelimiter>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

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
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
        }

        public ILexer<SubcomponentsDelimiter> Create()
        {
            // "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
            var innerLexer = alternationLexerFactory.Create(
                terminalLexerFactory.Create(@"!", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"$", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"&", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"'", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"(", StringComparer.Ordinal),
                terminalLexerFactory.Create(@")", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"*", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"+", StringComparer.Ordinal),
                terminalLexerFactory.Create(@",", StringComparer.Ordinal),
                terminalLexerFactory.Create(@";", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"=", StringComparer.Ordinal));

            // sub-delims
            return new SubcomponentsDelimiterLexer(innerLexer);
        }
    }
}
