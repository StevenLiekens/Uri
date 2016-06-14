using Txt.ABNF;

namespace UriSyntax.segment_nz
{
    public class SegmentNonZeroLength : Repetition
    {
        public SegmentNonZeroLength(Repetition repetition)
            : base(repetition)
        {
        }
    }
}
