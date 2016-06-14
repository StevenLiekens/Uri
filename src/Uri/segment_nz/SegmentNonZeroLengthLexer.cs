using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.segment_nz
{
    public sealed class SegmentNonZeroLengthLexer : CompositeLexer<Repetition, SegmentNonZeroLength>
    {
        public SegmentNonZeroLengthLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
