using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.segment;
using UriSyntax.segment_nz;

namespace UriSyntax.path_rootless
{
    public class PathRootlessLexerFactory : LexerFactory<PathRootless>
    {
        static PathRootlessLexerFactory()
        {
            Default = new PathRootlessLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                segment.SegmentLexerFactory.Default.Singleton(),
                segment_nz.SegmentNonZeroLengthLexerFactory.Default.Singleton());
        }

        public PathRootlessLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexerFactory<Segment> segmentLexerFactory,
            [NotNull] ILexerFactory<SegmentNonZeroLength> segmentNonZeroLengthLexerFactory)
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
            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentLexerFactory));
            }
            if (segmentNonZeroLengthLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentNonZeroLengthLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            SegmentLexerFactory = segmentLexerFactory;
            SegmentNonZeroLengthLexerFactory = segmentNonZeroLengthLexerFactory;
        }

        public static PathRootlessLexerFactory Default { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ILexerFactory<Segment> SegmentLexerFactory { get; }

        public ILexerFactory<SegmentNonZeroLength> SegmentNonZeroLengthLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<PathRootless> Create()
        {
            var innerLexer = ConcatenationLexerFactory.Create(
                SegmentNonZeroLengthLexerFactory.Create(),
                RepetitionLexerFactory.Create(
                    ConcatenationLexerFactory.Create(
                        TerminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                        SegmentLexerFactory.Create()),
                    0,
                    int.MaxValue));
            return new PathRootlessLexer(innerLexer);
        }
    }
}
