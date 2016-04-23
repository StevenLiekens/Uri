using System;
using Txt;
using Txt.ABNF;
using Uri.pct_encoded;
using Uri.sub_delims;
using Uri.unreserved;

namespace Uri.segment_nz_nc
{
    public class SegmentNonZeroLengthNoColonsLexerFactory : ILexerFactory<SegmentNonZeroLengthNoColons>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexerFactory<PercentEncoding> percentEncodingLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly ILexerFactory<Unreserved> unreservedLexerFactory;

        public SegmentNonZeroLengthNoColonsLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ILexerFactory<Unreserved> unreservedLexerFactory,
            ILexerFactory<PercentEncoding> percentEncodingLexerFactory,
            ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory,
            ITerminalLexerFactory terminalLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(repetitionLexerFactory));
            }

            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(alternationLexerFactory));
            }

            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(unreservedLexerFactory));
            }

            if (percentEncodingLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(percentEncodingLexerFactory));
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(subcomponentsDelimiterLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.unreservedLexerFactory = unreservedLexerFactory;
            this.percentEncodingLexerFactory = percentEncodingLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<SegmentNonZeroLengthNoColons> Create()
        {
            var alternationLexer = alternationLexerFactory.Create(
                unreservedLexerFactory.Create(),
                percentEncodingLexerFactory.Create(),
                subcomponentsDelimiterLexerFactory.Create(),
                terminalLexerFactory.Create(@"@", StringComparer.Ordinal));
            var segmentNonZeroLengthNoColonsRepetitionLexer = repetitionLexerFactory.Create(alternationLexer, 1, int.MaxValue);
            return new SegmentNonZeroLengthNoColonsLexer(segmentNonZeroLengthNoColonsRepetitionLexer);
        }
    }
}