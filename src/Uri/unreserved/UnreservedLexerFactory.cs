using System;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;

namespace Uri.unreserved
{
    public class UnreservedLexerFactory : ILexerFactory<Unreserved>
    {
        private readonly ILexerFactory<Alpha> alphaLexerFactory;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public UnreservedLexerFactory(
            ILexerFactory<Alpha> alphaLexerFactory,
            ILexerFactory<Digit> digitLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IAlternationLexerFactory alternationLexerFactory)
        {
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
            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alphaLexerFactory));
            }
            this.alphaLexerFactory = alphaLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
        }

        public ILexer<Unreserved> Create()
        {
            var unreservedAlternationLexer = alternationLexerFactory.Create(
                alphaLexerFactory.Create(),
                digitLexerFactory.Create(),
                terminalLexerFactory.Create(@"-", StringComparer.Ordinal),
                terminalLexerFactory.Create(@".", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"_", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"~", StringComparer.Ordinal));
            return new UnreservedLexer(unreservedAlternationLexer);
        }
    }
}
