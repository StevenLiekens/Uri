using Txt.ABNF;

namespace Uri.segment
{
    public class Segment : Repetition
    {
        public Segment(Repetition repetition)
            : base(repetition)
        {
        }
    }
}