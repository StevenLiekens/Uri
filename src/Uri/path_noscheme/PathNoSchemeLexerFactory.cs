using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.segment;
using Uri.segment_nz_nc;

namespace Uri.path_noscheme
{
    public class PathNoSchemeLexerFactory : ILexerFactory<PathNoScheme>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<Segment> segmentLexer;

        private readonly ILexer<SegmentNonZeroLengthNoColons> segmentNonZeroLengthNoColonsLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathNoSchemeLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Segment> segmentLexer,
            [NotNull] ILexer<SegmentNonZeroLengthNoColons> segmentNonZeroLengthNoColonsLexer)
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
            if (segmentNonZeroLengthNoColonsLexer == null)
            {
                throw new ArgumentNullException(nameof(segmentNonZeroLengthNoColonsLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.segmentLexer = segmentLexer;
            this.segmentNonZeroLengthNoColonsLexer = segmentNonZeroLengthNoColonsLexer;
        }

        public ILexer<PathNoScheme> Create()
        {
            // segment-nz-nc *( "/" segment )
            var innerLexer = concatenationLexerFactory.Create(
                segmentNonZeroLengthNoColonsLexer,
                repetitionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                        segmentLexer),
                    0,
                    int.MaxValue));

            // path-noscheme
            return new PathNoSchemeLexer(innerLexer);
        }
    }
}
