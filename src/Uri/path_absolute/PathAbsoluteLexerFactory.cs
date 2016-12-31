using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.segment;
using UriSyntax.segment_nz;

namespace UriSyntax.path_absolute
{
    public class PathAbsoluteLexerFactory : RuleLexerFactory<PathAbsolute>
    {
        static PathAbsoluteLexerFactory()
        {
            Default = new PathAbsoluteLexerFactory(
                segment.SegmentLexerFactory.Default.Singleton(),
                segment_nz.SegmentNonZeroLengthLexerFactory.Default.Singleton());
        }

        public PathAbsoluteLexerFactory(
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
        public static PathAbsoluteLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Segment> SegmentLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SegmentNonZeroLength> SegmentNonZeroLengthLexerFactory { get; }

        public override ILexer<PathAbsolute> Create()
        {
            // "/" [ segment-nz *( "/" segment ) ]
            var innerLexer = Concatenation.Create(
                Terminal.Create(@"/", StringComparer.Ordinal),
                Option.Create(
                    Concatenation.Create(
                        SegmentNonZeroLengthLexerFactory.Create(),
                        Repetition.Create(
                            Concatenation.Create(
                                Terminal.Create(@"/", StringComparer.Ordinal),
                                SegmentLexerFactory.Create()),
                            0,
                            int.MaxValue))));

            // path-absolute
            return new PathAbsoluteLexer(innerLexer);
        }
    }
}
