using Txt.ABNF;

namespace Uri.path_absolute
{
    public class PathAbsolute : Concatenation
    {
        public PathAbsolute(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}