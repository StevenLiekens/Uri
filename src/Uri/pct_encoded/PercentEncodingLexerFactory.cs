using System;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;

namespace Uri.pct_encoded
{
    public class PercentEncodingLexerFactory : ILexerFactory<PercentEncoding>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        public PercentEncodingLexerFactory(ITerminalLexerFactory terminalLexerFactory, ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory, IConcatenationLexerFactory concatenationLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalDigitLexerFactory));
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.hexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
        }

        public ILexer<PercentEncoding> Create()
        {
            var hexadecimalDigitLexer = hexadecimalDigitLexerFactory.Create();
            var percentEncodingAlternationLexer = concatenationLexerFactory.Create(
                terminalLexerFactory.Create(@"%", StringComparer.Ordinal),
                hexadecimalDigitLexer,
                hexadecimalDigitLexer);
            return new PercentEncodingLexer(percentEncodingAlternationLexer);
        }
    }
}
