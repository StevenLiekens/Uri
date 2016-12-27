using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.segment_nz
{
    public class SegmentNonZeroLength : Repetition
    {
        public SegmentNonZeroLength([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}