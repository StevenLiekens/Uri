using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pchar;

namespace UriSyntax.query
{
    public class QueryLexerFactory : RuleLexerFactory<Query>
    {
        static QueryLexerFactory()
        {
            Default = new QueryLexerFactory(pchar.PathCharacterLexerFactory.Default.Singleton());
        }

        public QueryLexerFactory(
            [NotNull] ILexerFactory<PathCharacter> pathCharacterLexerFactory)
        {
            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexerFactory));
            }
            PathCharacterLexerFactory = pathCharacterLexerFactory;
        }

        [NotNull]
        public static QueryLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<PathCharacter> PathCharacterLexerFactory { get; }

        public override ILexer<Query> Create()
        {
            var alternationLexer = Alternation.Create(
                PathCharacterLexerFactory.Create(),
                Terminal.Create(@"/", StringComparer.Ordinal),
                Terminal.Create(@"?", StringComparer.Ordinal));
            var fragmentRepetitionLexer = Repetition.Create(alternationLexer, 0, int.MaxValue);
            return new QueryLexer(fragmentRepetitionLexer);
        }
    }
}
