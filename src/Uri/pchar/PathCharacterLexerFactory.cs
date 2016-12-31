using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pct_encoded;
using UriSyntax.sub_delims;
using UriSyntax.unreserved;

namespace UriSyntax.pchar
{
    public class PathCharacterLexerFactory : RuleLexerFactory<PathCharacter>
    {
        static PathCharacterLexerFactory()
        {
            Default = new PathCharacterLexerFactory(
                unreserved.UnreservedLexerFactory.Default.Singleton(),
                pct_encoded.PercentEncodingLexerFactory.Default.Singleton(),
                sub_delims.SubcomponentsDelimiterLexerFactory.Default.Singleton());
        }

        public PathCharacterLexerFactory(
            [NotNull] ILexerFactory<Unreserved> unreservedLexerFactory,
            [NotNull] ILexerFactory<PercentEncoding> percentEncodingLexerFactory,
            [NotNull] ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
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
            UnreservedLexerFactory = unreservedLexerFactory;
            PercentEncodingLexerFactory = percentEncodingLexerFactory;
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
        }

        [NotNull]
        public static PathCharacterLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<PercentEncoding> PercentEncodingLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Unreserved> UnreservedLexerFactory { get; }

        public override ILexer<PathCharacter> Create()
        {
            var innerLexer = Alternation.Create(
                UnreservedLexerFactory.Create(),
                PercentEncodingLexerFactory.Create(),
                SubcomponentsDelimiterLexerFactory.Create(),
                Terminal.Create(@":", StringComparer.Ordinal),
                Terminal.Create(@"@", StringComparer.Ordinal));
            return new PathCharacterLexer(innerLexer);
        }
    }
}
