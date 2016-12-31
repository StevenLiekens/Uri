using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;
using Txt.Core;

namespace UriSyntax.pct_encoded
{
    public class PercentEncodingLexerFactory : RuleLexerFactory<PercentEncoding>
    {
        static PercentEncodingLexerFactory()
        {
            Default =
                new PercentEncodingLexerFactory(Txt.ABNF.Core.HEXDIG.HexadecimalDigitLexerFactory.Default.Singleton());
        }

        public PercentEncodingLexerFactory(
            [NotNull] ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory)
        {
            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalDigitLexerFactory));
            }
            HexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
        }

        [NotNull]
        public static PercentEncodingLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HexadecimalDigit> HexadecimalDigitLexerFactory { get; }

        public override ILexer<PercentEncoding> Create()
        {
            var hexDigitLexer = HexadecimalDigitLexerFactory.Create();
            var percentEncodingAlternationLexer = Concatenation.Create(
                Terminal.Create(@"%", StringComparer.Ordinal),
                hexDigitLexer,
                hexDigitLexer);
            return new PercentEncodingLexer(percentEncodingAlternationLexer);
        }
    }
}
