using System;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;
using Uri.sub_delims;
using Uri.unreserved;

namespace Uri.IPvFuture
{
    public class IPvFutureLexerFactory : ILexerFactory<IPvFuture>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexerFactory<Unreserved> unreservedLexerFactory;

        public IPvFutureLexerFactory(
            ITerminalLexerFactory terminalLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory,
            ILexerFactory<Unreserved> unreservedLexerFactory,
            ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalDigitLexerFactory));
            }
            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(unreservedLexerFactory));
            }
            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.hexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
            this.unreservedLexerFactory = unreservedLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
        }

        public ILexer<IPvFuture> Create()
        {
            // "v"
            var v = terminalLexerFactory.Create(@"v", StringComparer.OrdinalIgnoreCase);

            // HEXDIG
            var hexdig = hexadecimalDigitLexerFactory.Create();

            // "."
            var dot = terminalLexerFactory.Create(@".", StringComparer.Ordinal);

            // unreserved
            var unreserved = unreservedLexerFactory.Create();

            // sub-delims
            var subDelims = subcomponentsDelimiterLexerFactory.Create();

            // ":"
            var colon = terminalLexerFactory.Create(@":", StringComparer.Ordinal);

            // 1*HEXDIG
            var r = repetitionLexerFactory.Create(hexdig, 1, int.MaxValue);

            // unreserved / sub-delims / ":"
            var a = alternationLexerFactory.Create(unreserved, subDelims, colon);

            // 1*( unreserved / sub-delims / ":" )
            var s = repetitionLexerFactory.Create(a, 1, int.MaxValue);

            // "v" 1*HEXDIG "." 1*( unreserved / sub-delims / ":" )
            var innerLexer = concatenationLexerFactory.Create(v, r, dot, s);

            // IPvFuture
            return new IPvFutureLexer(innerLexer);
        }
    }
}
