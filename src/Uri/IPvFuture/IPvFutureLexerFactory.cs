using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;
using Txt.Core;
using UriSyntax.sub_delims;
using UriSyntax.unreserved;

namespace UriSyntax.IPvFuture
{
    public class IPvFutureLexerFactory : ILexerFactory<IPvFuture>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HexadecimalDigit> hexadecimalDigitLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<Unreserved> unreservedLexer;

        public IPvFutureLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<HexadecimalDigit> hexadecimalDigitLexer,
            [NotNull] ILexer<Unreserved> unreservedLexer,
            [NotNull] ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (hexadecimalDigitLexer == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalDigitLexer));
            }
            if (unreservedLexer == null)
            {
                throw new ArgumentNullException(nameof(unreservedLexer));
            }
            if (subcomponentsDelimiterLexer == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.hexadecimalDigitLexer = hexadecimalDigitLexer;
            this.unreservedLexer = unreservedLexer;
            this.subcomponentsDelimiterLexer = subcomponentsDelimiterLexer;
        }

        public ILexer<IPvFuture> Create()
        {
            // "v"
            var v = terminalLexerFactory.Create(@"v", StringComparer.OrdinalIgnoreCase);

            // "."
            var dot = terminalLexerFactory.Create(@".", StringComparer.Ordinal);

            // ":"
            var colon = terminalLexerFactory.Create(@":", StringComparer.Ordinal);

            // 1*HEXDIG
            var r = repetitionLexerFactory.Create(hexadecimalDigitLexer, 1, int.MaxValue);

            // unreserved / sub-delims / ":"
            var a = alternationLexerFactory.Create(unreservedLexer, subcomponentsDelimiterLexer, colon);

            // 1*( unreserved / sub-delims / ":" )
            var s = repetitionLexerFactory.Create(a, 1, int.MaxValue);

            // "v" 1*HEXDIG "." 1*( unreserved / sub-delims / ":" )
            var innerLexer = concatenationLexerFactory.Create(v, r, dot, s);

            // IPvFuture
            return new IPvFutureLexer(innerLexer);
        }
    }
}
