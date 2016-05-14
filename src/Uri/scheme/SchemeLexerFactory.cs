using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;

namespace Uri.scheme
{
    public class SchemeLexerFactory : ILexerFactory<Scheme>
    {
        private readonly ILexer<Alpha> alphaLexer;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Digit> digitLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public SchemeLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Alpha> alphaLexer,
            [NotNull] ILexer<Digit> digitLexer)
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
            if (alphaLexer == null)
            {
                throw new ArgumentNullException(nameof(alphaLexer));
            }
            if (digitLexer == null)
            {
                throw new ArgumentNullException(nameof(digitLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alphaLexer = alphaLexer;
            this.digitLexer = digitLexer;
        }

        public ILexer<Scheme> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                alphaLexer,
                repetitionLexerFactory.Create(
                    alternationLexerFactory.Create(
                        alphaLexer,
                        digitLexer,
                        terminalLexerFactory.Create(@"+", StringComparer.Ordinal),
                        terminalLexerFactory.Create(@"-", StringComparer.Ordinal),
                        terminalLexerFactory.Create(@".", StringComparer.Ordinal)),
                    0,
                    int.MaxValue));
            return new SchemeLexer(innerLexer);
        }
    }
}
