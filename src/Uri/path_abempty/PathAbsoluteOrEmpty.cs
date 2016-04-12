using Txt.ABNF;

namespace Uri.path_abempty
{
    public class PathAbsoluteOrEmpty : Repetition
    {
        public PathAbsoluteOrEmpty(Repetition repetition)
            : base(repetition)
        {
        }
    }
}