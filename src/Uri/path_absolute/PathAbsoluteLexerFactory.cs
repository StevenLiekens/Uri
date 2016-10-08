using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.segment;
using UriSyntax.segment_nz;

namespace UriSyntax.path_absolute
{
    public class PathAbsoluteLexerFactory : LexerFactory<PathAbsolute>
    {
        static PathAbsoluteLexerFactory()
        {
            Default = new PathAbsoluteLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                Txt.ABNF.OptionLexerFactory.Default,
                segment.SegmentLexerFactory.Default.Singleton(),
                segment_nz.SegmentNonZeroLengthLexerFactory.Default.Singleton());
        }

        public PathAbsoluteLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
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
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
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
            OptionLexerFactory = optionLexerFactory;
            SegmentLexerFactory = segmentLexerFactory;
            SegmentNonZeroLengthLexerFactory = segmentNonZeroLengthLexerFactory;
        }

        public static PathAbsoluteLexerFactory Default { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public IOptionLexerFactory OptionLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ILexerFactory<Segment> SegmentLexerFactory { get; }

        public ILexerFactory<SegmentNonZeroLength> SegmentNonZeroLengthLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<PathAbsolute> Create()
        {
            // "/" [ segment-nz *( "/" segment ) ]
            var innerLexer = ConcatenationLexerFactory.Create(
                TerminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                OptionLexerFactory.Create(
                    ConcatenationLexerFactory.Create(
                        SegmentNonZeroLengthLexerFactory.Create(),
                        RepetitionLexerFactory.Create(
                            ConcatenationLexerFactory.Create(
                                TerminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                                SegmentLexerFactory.Create()),
                            0,
                            int.MaxValue))));

            // path-absolute
            return new PathAbsoluteLexer(innerLexer);
        }
    }
}
