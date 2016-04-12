using Txt.ABNF;

namespace Uri.segment_nz_nc
{
    public class SegmentNonZeroLengthNoColons : Repetition
    {
        public SegmentNonZeroLengthNoColons(Repetition repetition)
            : base(repetition)
        {
        }
    }
}