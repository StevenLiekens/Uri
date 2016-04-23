using System;
using Txt;
using Txt.ABNF;

namespace Uri.sub_delims
{
    public class SubcomponentsDelimiterLexerFactory : ILexerFactory<SubcomponentsDelimiter>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public SubcomponentsDelimiterLexerFactory(
            ITerminalLexerFactory terminalLexerFactory,
            IAlternationLexerFactory alternationLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(alternationLexerFactory));
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
        }

        public ILexer<SubcomponentsDelimiter> Create()
        {
            ILexer[] a =
                {
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
                    terminalLexerFactory.Create(@"=", StringComparer.Ordinal)
                };

            // "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
            var b = alternationLexerFactory.Create(a);

            // sub-delims
            return new SubcomponentsDelimiterLexer(b);
        }
    }
}