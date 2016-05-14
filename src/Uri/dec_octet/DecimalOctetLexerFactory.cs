using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;

namespace Uri.dec_octet
{
    public class DecimalOctetLexerFactory : ILexerFactory<DecimalOctet>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Digit> digitLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public DecimalOctetLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Digit> digitLexer)
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
            if (digitLexer == null)
            {
                throw new ArgumentNullException(nameof(digitLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.digitLexer = digitLexer;
        }

        public ILexer<DecimalOctet> Create()
        {
            // %x30-35
            var a = valueRangeLexerFactory.Create('\x30', '\x35');

            // "25"
            var b = terminalLexerFactory.Create("25", StringComparer.Ordinal);

            // "25" %x30-35 
            var c = concatenationLexerFactory.Create(b, a);

            // DIGIT

            // %x30-34
            var e = valueRangeLexerFactory.Create('\x30', '\x34');

            // "2"
            var f = terminalLexerFactory.Create("2", StringComparer.Ordinal);

            // "2" %x30-34 DIGIT 
            var g = concatenationLexerFactory.Create(f, e, digitLexer);

            // 2DIGIT
            var h = repetitionLexerFactory.Create(digitLexer, 2, 2);

            // "1"
            var i = terminalLexerFactory.Create("1", StringComparer.Ordinal);

            // "1" 2DIGIT  
            var j = concatenationLexerFactory.Create(i, h);

            // %x31-39
            var k = valueRangeLexerFactory.Create('\x31', '\x39');

            // %x31-39 DIGIT 
            var l = concatenationLexerFactory.Create(k, digitLexer);

            // "25" %x30-35 / "2" %x30-34 DIGIT / "1" 2DIGIT / %x31-39 DIGIT / DIGIT
            var m = alternationLexerFactory.Create(c, g, j, l, digitLexer);

            // dec-octet
            return new DecimalOctetLexer(m);
        }
    }
}
