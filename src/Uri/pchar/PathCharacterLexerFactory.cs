using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pct_encoded;
using UriSyntax.sub_delims;
using UriSyntax.unreserved;

namespace UriSyntax.pchar
{
    public class PathCharacterLexerFactory : ILexerFactory<PathCharacter>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<PercentEncoding> percentEncodingLexer;

        private readonly ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<Unreserved> unreservedLexer;

        public PathCharacterLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<Unreserved> unreservedLexer,
            [NotNull] ILexer<PercentEncoding> percentEncodingLexer,
            [NotNull] ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (unreservedLexer == null)
            {
                throw new ArgumentNullException(nameof(unreservedLexer));
            }
            if (percentEncodingLexer == null)
            {
                throw new ArgumentNullException(nameof(percentEncodingLexer));
            }
            if (subcomponentsDelimiterLexer == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.unreservedLexer = unreservedLexer;
            this.percentEncodingLexer = percentEncodingLexer;
            this.subcomponentsDelimiterLexer = subcomponentsDelimiterLexer;
        }

        public ILexer<PathCharacter> Create()
        {
            var innerLexer = alternationLexerFactory.Create(
                unreservedLexer,
                percentEncodingLexer,
                subcomponentsDelimiterLexer,
                terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"@", StringComparer.Ordinal));
            return new PathCharacterLexer(innerLexer);
        }
    }
}
