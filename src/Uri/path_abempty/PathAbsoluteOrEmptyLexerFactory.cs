using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Uri.segment;

namespace Uri.path_abempty
{
    public class PathAbsoluteOrEmptyLexerFactory : ILexerFactory<PathAbsoluteOrEmpty>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<Segment> segmentLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathAbsoluteOrEmptyLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Segment> segmentLexer)
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
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.segmentLexer = segmentLexer;
        }

        public ILexer<PathAbsoluteOrEmpty> Create()
        {
            // "/"
            var a = terminalLexerFactory.Create(@"/", StringComparer.Ordinal);

            // "/" segment
            var c = concatenationLexerFactory.Create(a, segmentLexer);

            // *( "/" segment )
            var innerLexer = repetitionLexerFactory.Create(c, 0, int.MaxValue);

            // path-abempty
            return new PathAbsoluteOrEmptyLexer(innerLexer);
        }
    }
}
