using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Uri.segment;
using Uri.segment_nz;

namespace Uri.path_rootless
{
    public class PathRootlessLexerFactory : ILexerFactory<PathRootless>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<Segment> segmentLexer;

        private readonly ILexer<SegmentNonZeroLength> segmentNonZeroLengthLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathRootlessLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Segment> segmentLexer,
            [NotNull] ILexer<SegmentNonZeroLength> segmentNonZeroLengthLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (segmentLexer == null)
            {
                throw new ArgumentNullException(nameof(segmentLexer));
            }
            if (segmentNonZeroLengthLexer == null)
            {
                throw new ArgumentNullException(nameof(segmentNonZeroLengthLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.segmentLexer = segmentLexer;
            this.segmentNonZeroLengthLexer = segmentNonZeroLengthLexer;
        }

        public ILexer<PathRootless> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                segmentNonZeroLengthLexer,
                repetitionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                        segmentLexer),
                    0,
                    int.MaxValue));
            return new PathRootlessLexer(innerLexer);
        }
    }
}
