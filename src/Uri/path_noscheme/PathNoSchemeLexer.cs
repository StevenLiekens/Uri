using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path_noscheme
{
    public sealed class PathNoSchemeLexer : CompositeLexer<Concatenation, PathNoScheme>
    {
        public PathNoSchemeLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
