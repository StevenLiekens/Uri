using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.pchar;

namespace Uri.query
{
    public class QueryLexerFactory : ILexerFactory<Query>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<PathCharacter> pathCharacterLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public QueryLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<PathCharacter> pathCharacterLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (pathCharacterLexer == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.pathCharacterLexer = pathCharacterLexer;
        }

        public ILexer<Query> Create()
        {
            var alternationLexer = alternationLexerFactory.Create(
                pathCharacterLexer,
                terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"?", StringComparer.Ordinal));
            var fragmentRepetitionLexer = repetitionLexerFactory.Create(alternationLexer, 0, int.MaxValue);
            return new QueryLexer(fragmentRepetitionLexer);
        }
    }
}
