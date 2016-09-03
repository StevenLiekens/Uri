using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pchar;

namespace UriSyntax.path_empty
{
    public class PathEmptyLexerFactory : ILexerFactory<PathEmpty>
    {
        private readonly ILexer<PathCharacter> pathCharacterLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public PathEmptyLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<PathCharacter> pathCharacterLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (pathCharacterLexer == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.pathCharacterLexer = pathCharacterLexer;
        }

        public ILexer<PathEmpty> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(pathCharacterLexer, 0, 0);
            return new PathEmptyLexer(innerLexer);
        }
    }
}
