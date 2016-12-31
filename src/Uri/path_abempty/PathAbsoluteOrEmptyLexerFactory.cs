using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.segment;

namespace UriSyntax.path_abempty
{
    public class PathAbsoluteOrEmptyLexerFactory : RuleLexerFactory<PathAbsoluteOrEmpty>
    {
        static PathAbsoluteOrEmptyLexerFactory()
        {
            Default = new PathAbsoluteOrEmptyLexerFactory(segment.SegmentLexerFactory.Default.Singleton());
        }

        public PathAbsoluteOrEmptyLexerFactory(
            [NotNull] ILexerFactory<Segment> segmentLexerFactory)
        {
            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentLexerFactory));
            }
            SegmentLexerFactory = segmentLexerFactory;
        }

        [NotNull]
        public static PathAbsoluteOrEmptyLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Segment> SegmentLexerFactory { get; }

        public override ILexer<PathAbsoluteOrEmpty> Create()
        {
            // "/"
            var a = Terminal.Create(@"/", StringComparer.Ordinal);

            // "/" segment
            var c = Concatenation.Create(a, SegmentLexerFactory.Create());

            // *( "/" segment )
            var innerLexer = Repetition.Create(c, 0, int.MaxValue);

            // path-abempty
            return new PathAbsoluteOrEmptyLexer(innerLexer);
        }
    }
}
