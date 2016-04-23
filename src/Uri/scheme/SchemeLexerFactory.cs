using System;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;

namespace Uri.scheme
{
    public class SchemeLexerFactory : ILexerFactory<Scheme>
    {
        private readonly ILexerFactory<Alpha> alphaLexerFactory;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public SchemeLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<Alpha> alphaLexerFactory,
            ILexerFactory<Digit> digitLexerFactory,
            ITerminalLexerFactory terminalLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alphaLexerFactory));
            }
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alphaLexerFactory = alphaLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<Scheme> Create()
        {
            var alpha = alphaLexerFactory.Create();
            var digit = digitLexerFactory.Create();
            var plus = terminalLexerFactory.Create(@"+", StringComparer.Ordinal);
            var minus = terminalLexerFactory.Create(@"-", StringComparer.Ordinal);
            var dot = terminalLexerFactory.Create(@".", StringComparer.Ordinal);
            var alt = alternationLexerFactory.Create(alpha, digit, plus, minus, dot);
            var rep = repetitionLexerFactory.Create(alt, 0, int.MaxValue);
            var innerLexer = concatenationLexerFactory.Create(alpha, rep);
            return new SchemeLexer(innerLexer);
        }
    }
}
