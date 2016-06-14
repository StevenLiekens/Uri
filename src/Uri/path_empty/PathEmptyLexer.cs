using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path_empty
{
    public sealed class PathEmptyLexer : CompositeLexer<Terminal, PathEmpty>
    {
        public PathEmptyLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
