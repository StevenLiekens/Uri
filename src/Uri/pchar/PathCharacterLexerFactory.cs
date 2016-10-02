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
                unreserved.UnreservedLexerFactory.Default,
                pct_encoded.PercentEncodingLexerFactory.Default,
                sub_delims.SubcomponentsDelimiterLexerFactory.Default);
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
            UnreservedLexerFactory = unreservedLexerFactory.Singleton();
            PercentEncodingLexerFactory = percentEncodingLexerFactory.Singleton();
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory.Singleton();
        }

        public static PathCharacterLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public ILexerFactory<PercentEncoding> PercentEncodingLexerFactory { get; }

        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

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
