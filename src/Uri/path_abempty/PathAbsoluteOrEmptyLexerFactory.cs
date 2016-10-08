using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.segment;

namespace UriSyntax.path_abempty
{
    public class PathAbsoluteOrEmptyLexerFactory : LexerFactory<PathAbsoluteOrEmpty>
    {
        static PathAbsoluteOrEmptyLexerFactory()
        {
            Default = new PathAbsoluteOrEmptyLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                segment.SegmentLexerFactory.Default.Singleton());
        }

        public PathAbsoluteOrEmptyLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexerFactory<Segment> segmentLexerFactory)
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
            TerminalLexerFactory = terminalLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            SegmentLexerFactory = segmentLexerFactory;
        }

        public static PathAbsoluteOrEmptyLexerFactory Default { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ILexerFactory<Segment> SegmentLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<PathAbsoluteOrEmpty> Create()
        {
            // "/"
            var a = TerminalLexerFactory.Create(@"/", StringComparer.Ordinal);

            // "/" segment
            var c = ConcatenationLexerFactory.Create(a, SegmentLexerFactory.Create());

            // *( "/" segment )
            var innerLexer = RepetitionLexerFactory.Create(c, 0, int.MaxValue);

            // path-abempty
            return new PathAbsoluteOrEmptyLexer(innerLexer);
        }
    }
}
