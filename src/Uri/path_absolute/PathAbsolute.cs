using Txt.ABNF;

namespace UriSyntax.path_absolute
{
    public class PathAbsolute : Concatenation
    {
        public PathAbsolute(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
