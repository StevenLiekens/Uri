using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.segment;
using UriSyntax.segment_nz_nc;

namespace UriSyntax.path_noscheme
{
    public class PathNoSchemeLexerFactory : RuleLexerFactory<PathNoScheme>
    {
        static PathNoSchemeLexerFactory()
        {
            Default = new PathNoSchemeLexerFactory(
                segment.SegmentLexerFactory.Default.Singleton(),
                segment_nz_nc.SegmentNonZeroLengthNoColonsLexerFactory.Default.Singleton());
        }

        public PathNoSchemeLexerFactory(
            [NotNull] ILexerFactory<Segment> segmentLexerFactory,
            [NotNull] ILexerFactory<SegmentNonZeroLengthNoColons> segmentNonZeroLengthNoColonsLexerFactory)
        {
            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentLexerFactory));
            }
            if (segmentNonZeroLengthNoColonsLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentNonZeroLengthNoColonsLexerFactory));
            }
            SegmentLexerFactory = segmentLexerFactory;
            SegmentNonZeroLengthNoColonsLexerFactory = segmentNonZeroLengthNoColonsLexerFactory;
        }

        [NotNull]
        public static PathNoSchemeLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Segment> SegmentLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SegmentNonZeroLengthNoColons> SegmentNonZeroLengthNoColonsLexerFactory { get; }

        public override ILexer<PathNoScheme> Create()
        {
            // segment-nz-nc *( "/" segment )
            var innerLexer = Concatenation.Create(
                SegmentNonZeroLengthNoColonsLexerFactory.Create(),
                Repetition.Create(
                    Concatenation.Create(
                        Terminal.Create(@"/", StringComparer.Ordinal),
                        SegmentLexerFactory.Create()),
                    0,
                    int.MaxValue));

            // path-noscheme
            return new PathNoSchemeLexer(innerLexer);
        }
    }
}
