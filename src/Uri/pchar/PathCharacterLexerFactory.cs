using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pct_encoded;
using UriSyntax.sub_delims;
using UriSyntax.unreserved;

namespace UriSyntax.pchar
{
    public class PathCharacterLexerFactory : LexerFactory<PathCharacter>
    {
        static PathCharacterLexerFactory()
        {
            Default = new PathCharacterLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                unreserved.UnreservedLexerFactory.Default.Singleton(),
                pct_encoded.PercentEncodingLexerFactory.Default.Singleton(),
                sub_delims.SubcomponentsDelimiterLexerFactory.Default.Singleton());
        }

        public PathCharacterLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexerFactory<Unreserved> unreservedLexerFactory,
            [NotNull] ILexerFactory<PercentEncoding> percentEncodingLexerFactory,
            [NotNull] ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
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
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            UnreservedLexerFactory = unreservedLexerFactory;
            PercentEncodingLexerFactory = percentEncodingLexerFactory;
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
        }

        [NotNull]
        public static PathCharacterLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PercentEncoding> PercentEncodingLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Unreserved> UnreservedLexerFactory { get; }

        public override ILexer<PathCharacter> Create()
        {
            var innerLexer = AlternationLexerFactory.Create(
                UnreservedLexerFactory.Create(),
                PercentEncodingLexerFactory.Create(),
                SubcomponentsDelimiterLexerFactory.Create(),
                TerminalLexerFactory.Create(@":", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"@", StringComparer.Ordinal));
            return new PathCharacterLexer(innerLexer);
        }
    }
}
