using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace UriSyntax.dec_octet
{
    public class DecimalOctetLexerFactory : LexerFactory<DecimalOctet>
    {
        static DecimalOctetLexerFactory()
        {
            Default = new DecimalOctetLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ValueRangeLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public DecimalOctetLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
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
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            ValueRangeLexerFactory = valueRangeLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            DigitLexerFactory = digitLexerFactory;
        }

        public static DecimalOctetLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<Digit> DigitLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public IValueRangeLexerFactory ValueRangeLexerFactory { get; }

        public override ILexer<DecimalOctet> Create()
        {
            // %x30-35
            var a = ValueRangeLexerFactory.Create('\x30', '\x35');

            // "25"
            var b = TerminalLexerFactory.Create("25", StringComparer.Ordinal);

            // "25" %x30-35 
            var c = ConcatenationLexerFactory.Create(b, a);

            // DIGIT

            // %x30-34
            var e = ValueRangeLexerFactory.Create('\x30', '\x34');

            // "2"
            var f = TerminalLexerFactory.Create("2", StringComparer.Ordinal);

            // "2" %x30-34 DIGIT 
            var digitLexer = DigitLexerFactory.Create();
            var g = ConcatenationLexerFactory.Create(f, e, digitLexer);

            // 2DIGIT
            var h = RepetitionLexerFactory.Create(digitLexer, 2, 2);

            // "1"
            var i = TerminalLexerFactory.Create("1", StringComparer.Ordinal);

            // "1" 2DIGIT  
            var j = ConcatenationLexerFactory.Create(i, h);

            // %x31-39
            var k = ValueRangeLexerFactory.Create('\x31', '\x39');

            // %x31-39 DIGIT 
            var l = ConcatenationLexerFactory.Create(k, digitLexer);

            // "25" %x30-35 / "2" %x30-34 DIGIT / "1" 2DIGIT / %x31-39 DIGIT / DIGIT
            var m = AlternationLexerFactory.Create(c, g, j, l, digitLexer);

            // dec-octet
            return new DecimalOctetLexer(m);
        }
    }
}
