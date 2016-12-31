using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.segment;
using UriSyntax.segment_nz;

namespace UriSyntax.path_rootless
{
    public class PathRootlessLexerFactory : RuleLexerFactory<PathRootless>
    {
        static PathRootlessLexerFactory()
        {
            Default = new PathRootlessLexerFactory(
                segment.SegmentLexerFactory.Default.Singleton(),
                segment_nz.SegmentNonZeroLengthLexerFactory.Default.Singleton());
        }

        public PathRootlessLexerFactory(
            [NotNull] ILexerFactory<Segment> segmentLexerFactory,
            [NotNull] ILexerFactory<SegmentNonZeroLength> segmentNonZeroLengthLexerFactory)
        {
            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentLexerFactory));
            }
            if (segmentNonZeroLengthLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentNonZeroLengthLexerFactory));
            }
            SegmentLexerFactory = segmentLexerFactory;
            SegmentNonZeroLengthLexerFactory = segmentNonZeroLengthLexerFactory;
        }

        [NotNull]
        public static PathRootlessLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Segment> SegmentLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SegmentNonZeroLength> SegmentNonZeroLengthLexerFactory { get; }

        public override ILexer<PathRootless> Create()
        {
            var innerLexer = Concatenation.Create(
                SegmentNonZeroLengthLexerFactory.Create(),
                Repetition.Create(
                    Concatenation.Create(
                        Terminal.Create(@"/", StringComparer.Ordinal),
                        SegmentLexerFactory.Create()),
                    0,
                    int.MaxValue));
            return new PathRootlessLexer(innerLexer);
        }
    }
}
