using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.segment
{
    public sealed class SegmentLexer : CompositeLexer<Repetition, Segment>
    {
        public SegmentLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
