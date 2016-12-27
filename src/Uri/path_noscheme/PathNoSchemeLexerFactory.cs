using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.segment;
using UriSyntax.segment_nz_nc;

namespace UriSyntax.path_noscheme
{
    public class PathNoSchemeLexerFactory : LexerFactory<PathNoScheme>
    {
        static PathNoSchemeLexerFactory()
        {
            Default = new PathNoSchemeLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                segment.SegmentLexerFactory.Default.Singleton(),
                segment_nz_nc.SegmentNonZeroLengthNoColonsLexerFactory.Default.Singleton());
        }

        public PathNoSchemeLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexerFactory<Segment> segmentLexerFactory,
            [NotNull] ILexerFactory<SegmentNonZeroLengthNoColons> segmentNonZeroLengthNoColonsLexerFactory)
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
            if (segmentNonZeroLengthNoColonsLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentNonZeroLengthNoColonsLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            SegmentLexerFactory = segmentLexerFactory;
            SegmentNonZeroLengthNoColonsLexerFactory = segmentNonZeroLengthNoColonsLexerFactory;
        }

        [NotNull]
        public static PathNoSchemeLexerFactory Default { get; }

        [NotNull]
        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        [NotNull]
        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Segment> SegmentLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SegmentNonZeroLengthNoColons> SegmentNonZeroLengthNoColonsLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<PathNoScheme> Create()
        {
            // segment-nz-nc *( "/" segment )
            var innerLexer = ConcatenationLexerFactory.Create(
                SegmentNonZeroLengthNoColonsLexerFactory.Create(),
                RepetitionLexerFactory.Create(
                    ConcatenationLexerFactory.Create(
                        TerminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                        SegmentLexerFactory.Create()),
                    0,
                    int.MaxValue));

            // path-noscheme
            return new PathNoSchemeLexer(innerLexer);
        }
    }
}
