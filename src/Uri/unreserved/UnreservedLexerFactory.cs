using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;

namespace Uri.unreserved
{
    public class UnreservedLexerFactory : ILexerFactory<Unreserved>
    {
        private readonly ILexer<Alpha> alphaLexer;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<Digit> digitLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public UnreservedLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
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
            this.alphaLexer = alphaLexer;
            this.digitLexer = digitLexer;
        }

        public ILexer<Unreserved> Create()
        {
            var innerLexer = alternationLexerFactory.Create(
                alphaLexer,
                digitLexer,
                terminalLexerFactory.Create(@"-", StringComparer.Ordinal),
                terminalLexerFactory.Create(@".", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"_", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"~", StringComparer.Ordinal));
            return new UnreservedLexer(innerLexer);
        }
    }
}
