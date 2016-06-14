using Txt.ABNF;

namespace UriSyntax.segment_nz_nc
{
    public class SegmentNonZeroLengthNoColons : Repetition
    {
        public SegmentNonZeroLengthNoColons(Repetition repetition)
            : base(repetition)
        {
        }
    }
}
