using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.segment;
using Uri.segment_nz;

namespace Uri.path_absolute
{
    public class PathAbsoluteLexerFactory : ILexerFactory<PathAbsolute>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<Segment> segmentLexer;

        private readonly ILexer<SegmentNonZeroLength> segmentNonZeroLengthLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathAbsoluteLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
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
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
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
            this.optionLexerFactory = optionLexerFactory;
            this.segmentLexer = segmentLexer;
            this.segmentNonZeroLengthLexer = segmentNonZeroLengthLexer;
        }

        public ILexer<PathAbsolute> Create()
        {
            // "/" [ segment-nz *( "/" segment ) ]
            var innerLexer = concatenationLexerFactory.Create(
                terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                optionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        segmentNonZeroLengthLexer,
                        repetitionLexerFactory.Create(
                            concatenationLexerFactory.Create(
                                terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                                segmentLexer),
                            0,
                            int.MaxValue))));

            // path-absolute
            return new PathAbsoluteLexer(innerLexer);
        }
    }
}
