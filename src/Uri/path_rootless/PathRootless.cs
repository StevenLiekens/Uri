using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.path_rootless
{
    public class PathRootless : Concatenation
    {
        public PathRootless([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}