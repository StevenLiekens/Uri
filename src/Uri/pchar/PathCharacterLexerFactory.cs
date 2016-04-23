using System;
using Txt;
using Txt.ABNF;
using Uri.pct_encoded;
using Uri.sub_delims;
using Uri.unreserved;

namespace Uri.pchar
{
    public class PathCharacterLexerFactory : ILexerFactory<PathCharacter>
    {
        private readonly ILexerFactory<Unreserved> unreservedLexerFactory;

        private readonly ILexerFactory<PercentEncoding> percentEncodingLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        public PathCharacterLexerFactory(ILexerFactory<Unreserved> unreservedLexerFactory, ILexerFactory<PercentEncoding> percentEncodingLexerFactory, ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory, ITerminalLexerFactory terminalLexerFactory, IAlternationLexerFactory alternationLexerFactory)
        {
            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(unreservedLexerFactory));
            }

            if (percentEncodingLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(percentEncodingLexerFactory));
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }

            this.unreservedLexerFactory = unreservedLexerFactory;
            this.percentEncodingLexerFactory = percentEncodingLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
        }

        public ILexer<PathCharacter> Create()
        {
            var pathCharacterAlternationLexer = alternationLexerFactory.Create(
                unreservedLexerFactory.Create(),
                percentEncodingLexerFactory.Create(),
                subcomponentsDelimiterLexerFactory.Create(),
                terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"@", StringComparer.Ordinal));
            return new PathCharacterLexer(pathCharacterAlternationLexer);
        }
    }
}
