using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pct_encoded;
using UriSyntax.sub_delims;
using UriSyntax.unreserved;

namespace UriSyntax.segment_nz_nc
{
    public class SegmentNonZeroLengthNoColonsLexerFactory : RuleLexerFactory<SegmentNonZeroLengthNoColons>
    {
        static SegmentNonZeroLengthNoColonsLexerFactory()
        {
            Default = new SegmentNonZeroLengthNoColonsLexerFactory(
                unreserved.UnreservedLexerFactory.Default.Singleton(),
                pct_encoded.PercentEncodingLexerFactory.Default.Singleton(),
                sub_delims.SubcomponentsDelimiterLexerFactory.Default.Singleton());
        }

        public SegmentNonZeroLengthNoColonsLexerFactory(
            [NotNull] ILexerFactory<Unreserved> unreservedLexerFactory,
            [NotNull] ILexerFactory<PercentEncoding> percentEncodingLexerFactory,
            [NotNull] ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(unreservedLexerFactory));
            }
            if (percentEncodingLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(percentEncodingLexerFactory));
            }
            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }
            UnreservedLexerFactory = unreservedLexerFactory;
            PercentEncodingLexerFactory = percentEncodingLexerFactory;
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
        }

        [NotNull]
        public static SegmentNonZeroLengthNoColonsLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<PercentEncoding> PercentEncodingLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Unreserved> UnreservedLexerFactory { get; }

        public override ILexer<SegmentNonZeroLengthNoColons> Create()
        {
            var alternationLexer = Alternation.Create(
                UnreservedLexerFactory.Create(),
                PercentEncodingLexerFactory.Create(),
                SubcomponentsDelimiterLexerFactory.Create(),
                Terminal.Create(@"@", StringComparer.Ordinal));
            var segmentNonZeroLengthNoColonsRepetitionLexer = Repetition.Create(
                alternationLexer,
                1,
                int.MaxValue);
            return new SegmentNonZeroLengthNoColonsLexer(segmentNonZeroLengthNoColonsRepetitionLexer);
        }
    }
}
