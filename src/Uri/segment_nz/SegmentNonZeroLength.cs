using Txt.ABNF;

namespace Uri.segment_nz
{
    public class SegmentNonZeroLength : Repetition
    {
        public SegmentNonZeroLength(Repetition repetition)
            : base(repetition)
        {
        }
    }
}