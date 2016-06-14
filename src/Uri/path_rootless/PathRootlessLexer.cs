using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path_rootless
{
    public sealed class PathRootlessLexer : CompositeLexer<Concatenation, PathRootless>
    {
        public PathRootlessLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
