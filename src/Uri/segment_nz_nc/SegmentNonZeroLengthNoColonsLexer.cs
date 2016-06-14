using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.segment_nz_nc
{
    public sealed class SegmentNonZeroLengthNoColonsLexer : CompositeLexer<Repetition, SegmentNonZeroLengthNoColons>
    {
        public SegmentNonZeroLengthNoColonsLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
