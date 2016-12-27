using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;
using Txt.Core;

namespace UriSyntax.pct_encoded
{
    public class PercentEncodingLexerFactory : LexerFactory<PercentEncoding>
    {
        static PercentEncodingLexerFactory()
        {
            Default = new PercentEncodingLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.Core.HEXDIG.HexadecimalDigitLexerFactory.Default.Singleton());
        }

        public PercentEncodingLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalDigitLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            HexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
        }

        [NotNull]
        public static PercentEncodingLexerFactory Default { get; }

        [NotNull]
        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<HexadecimalDigit> HexadecimalDigitLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<PercentEncoding> Create()
        {
            var hexDigitLexer = HexadecimalDigitLexerFactory.Create();
            var percentEncodingAlternationLexer = ConcatenationLexerFactory.Create(
                TerminalLexerFactory.Create(@"%", StringComparer.Ordinal),
                hexDigitLexer,
                hexDigitLexer);
            return new PercentEncodingLexer(percentEncodingAlternationLexer);
        }
    }
}
