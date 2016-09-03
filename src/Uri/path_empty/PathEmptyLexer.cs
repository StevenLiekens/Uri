using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path_empty
{
    public sealed class PathEmptyLexer : CompositeLexer<Repetition, PathEmpty>
    {
        public PathEmptyLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
