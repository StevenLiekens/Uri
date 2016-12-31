using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace UriSyntax.dec_octet
{
    public class DecimalOctetLexerFactory : RuleLexerFactory<DecimalOctet>
    {
        static DecimalOctetLexerFactory()
        {
            Default = new DecimalOctetLexerFactory(Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public DecimalOctetLexerFactory(
            [NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            DigitLexerFactory = digitLexerFactory;
        }

        [NotNull]
        public static DecimalOctetLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; }

        public override ILexer<DecimalOctet> Create()
        {
            // %x30-35
            var a = ValueRange.Create('\x30', '\x35');

            // "25"
            var b = Terminal.Create("25", StringComparer.Ordinal);

            // "25" %x30-35 
            var c = Concatenation.Create(b, a);

            // DIGIT

            // %x30-34
            var e = ValueRange.Create('\x30', '\x34');

            // "2"
            var f = Terminal.Create("2", StringComparer.Ordinal);

            // "2" %x30-34 DIGIT 
            var digitLexer = DigitLexerFactory.Create();
            var g = Concatenation.Create(f, e, digitLexer);

            // 2DIGIT
            var h = Repetition.Create(digitLexer, 2, 2);

            // "1"
            var i = Terminal.Create("1", StringComparer.Ordinal);

            // "1" 2DIGIT  
            var j = Concatenation.Create(i, h);

            // %x31-39
            var k = ValueRange.Create('\x31', '\x39');

            // %x31-39 DIGIT 
            var l = Concatenation.Create(k, digitLexer);

            // "25" %x30-35 / "2" %x30-34 DIGIT / "1" 2DIGIT / %x31-39 DIGIT / DIGIT
            var m = Alternation.Create(c, g, j, l, digitLexer);

            // dec-octet
            return new DecimalOctetLexer(m);
        }
    }
}
