using Txt.ABNF;

namespace UriSyntax.path_rootless
{
    public class PathRootless : Concatenation
    {
        public PathRootless(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
