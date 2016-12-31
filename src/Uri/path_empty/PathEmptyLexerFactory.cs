using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pchar;

namespace UriSyntax.path_empty
{
    public class PathEmptyLexerFactory : RuleLexerFactory<PathEmpty>
    {
        static PathEmptyLexerFactory()
        {
            Default = new PathEmptyLexerFactory(pchar.PathCharacterLexerFactory.Default.Singleton());
        }

        public PathEmptyLexerFactory(
            [NotNull] ILexerFactory<PathCharacter> pathCharacterLexerFactory)
        {
            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexerFactory));
            }
            PathCharacterLexerFactory = pathCharacterLexerFactory;
        }

        [NotNull]
        public static PathEmptyLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<PathCharacter> PathCharacterLexerFactory { get; }

        public override ILexer<PathEmpty> Create()
        {
            var innerLexer = Repetition.Create(PathCharacterLexerFactory.Create(), 0, 0);
            return new PathEmptyLexer(innerLexer);
        }
    }
}
