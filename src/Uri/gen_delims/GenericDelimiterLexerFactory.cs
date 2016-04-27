using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;

namespace Uri.gen_delims
{
    public class GenericDelimiterLexerFactory : ILexerFactory<GenericDelimiter>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public GenericDelimiterLexerFactory(
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

        public ILexer<GenericDelimiter> Create()
        {
            // ":" / "/" / "?" / "#" / "[" / "]" / "@"
            var innerLexer = alternationLexerFactory.Create(
                terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"[", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"]", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"@", StringComparer.Ordinal));

            // gen-delims
            return new GenericDelimiterLexer(innerLexer);
        }
    }
}
