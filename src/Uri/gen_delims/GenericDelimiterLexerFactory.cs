using System;
using Txt;
using Txt.ABNF;

namespace Uri.gen_delims
{
    public class GenericDelimiterLexerFactory : ILexerFactory<GenericDelimiter>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public GenericDelimiterLexerFactory(ITerminalLexerFactory terminalLexerFactory, IAlternationLexerFactory alternationLexerFactory)
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

        public ILexer<GenericDelimiter> Create()
        {
            ILexer[] a =
                {
                    terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"[", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"]", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"@", StringComparer.Ordinal)
                };

            // ":" / "/" / "?" / "#" / "[" / "]" / "@"
            var b = alternationLexerFactory.Create(a);

            // gen-delims
            return new GenericDelimiterLexer(b);
        }
    }
}
