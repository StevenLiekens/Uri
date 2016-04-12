using Txt.ABNF;

namespace Uri.path_rootless
{
    public class PathRootless : Concatenation
    {
        public PathRootless(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}