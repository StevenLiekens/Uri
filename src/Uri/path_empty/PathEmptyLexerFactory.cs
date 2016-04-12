using System;
using Txt;
using Txt.ABNF;

namespace Uri.path_empty
{
    public class PathEmptyLexerFactory : ILexerFactory<PathEmpty>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathEmptyLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<PathEmpty> Create()
        {
            var innerLexer = terminalLexerFactory.Create(string.Empty, StringComparer.Ordinal);
            return new PathEmptyLexer(innerLexer);
        }
    }
}