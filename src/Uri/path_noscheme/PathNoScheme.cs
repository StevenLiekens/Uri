using Txt.ABNF;

namespace Uri.path_noscheme
{
    public class PathNoScheme : Concatenation
    {
        public PathNoScheme(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}