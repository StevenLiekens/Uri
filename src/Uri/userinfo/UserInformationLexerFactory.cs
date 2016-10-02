using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pct_encoded;
using UriSyntax.sub_delims;
using UriSyntax.unreserved;

namespace UriSyntax.userinfo
{
    public class UserInformationLexerFactory : LexerFactory<UserInformation>
    {
        static UserInformationLexerFactory()
        {
            Default = new UserInformationLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                unreserved.UnreservedLexerFactory.Default,
                pct_encoded.PercentEncodingLexerFactory.Default,
                sub_delims.SubcomponentsDelimiterLexerFactory.Default);
        }

        public UserInformationLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
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
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
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
            RepetitionLexerFactory = repetitionLexerFactory;
            UnreservedLexerFactory = unreservedLexerFactory.Singleton();
            PercentEncodingLexerFactory = percentEncodingLexerFactory.Singleton();
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory.Singleton();
        }

        public static UserInformationLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public ILexerFactory<PercentEncoding> PercentEncodingLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public ILexerFactory<Unreserved> UnreservedLexerFactory { get; }

        public override ILexer<UserInformation> Create()
        {
            var innerLexer = RepetitionLexerFactory.Create(
                AlternationLexerFactory.Create(
                    UnreservedLexerFactory.Create(),
                    PercentEncodingLexerFactory.Create(),
                    SubcomponentsDelimiterLexerFactory.Create(),
                    TerminalLexerFactory.Create(@":", StringComparer.Ordinal)),
                0,
                int.MaxValue);
            return new UserInformationLexer(innerLexer);
        }
    }
}
