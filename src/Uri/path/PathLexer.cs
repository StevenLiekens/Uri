using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path
{
    public sealed class PathLexer : CompositeLexer<Alternation, Path>
    {
        public PathLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
