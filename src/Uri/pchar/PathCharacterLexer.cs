using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.pchar
{
    public sealed class PathCharacterLexer : CompositeLexer<Alternation, PathCharacter>
    {
        public PathCharacterLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
