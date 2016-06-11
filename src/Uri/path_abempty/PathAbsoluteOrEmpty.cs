using Txt.ABNF;

namespace UriSyntax.path_abempty
{
    public class PathAbsoluteOrEmpty : Repetition
    {
        public PathAbsoluteOrEmpty(Repetition repetition)
            : base(repetition)
        {
        }
    }
}