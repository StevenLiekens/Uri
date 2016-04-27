using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;

namespace Uri.pct_encoded
{
    public class PercentEncodingLexerFactory : ILexerFactory<PercentEncoding>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HexadecimalDigit> hexadecimalDigitLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PercentEncodingLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<HexadecimalDigit> hexadecimalDigitLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (hexadecimalDigitLexer == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalDigitLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.hexadecimalDigitLexer = hexadecimalDigitLexer;
        }

        public ILexer<PercentEncoding> Create()
        {
            var percentEncodingAlternationLexer = concatenationLexerFactory.Create(
                terminalLexerFactory.Create(@"%", StringComparer.Ordinal),
                hexadecimalDigitLexer,
                hexadecimalDigitLexer);
            return new PercentEncodingLexer(percentEncodingAlternationLexer);
        }
    }
}
