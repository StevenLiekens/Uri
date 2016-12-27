using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.segment_nz_nc
{
    public class SegmentNonZeroLengthNoColons : Repetition
    {
        public SegmentNonZeroLengthNoColons([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}