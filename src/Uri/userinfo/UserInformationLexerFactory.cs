using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Uri.pct_encoded;
using Uri.sub_delims;
using Uri.unreserved;

namespace Uri.userinfo
{
    public class UserInformationLexerFactory : ILexerFactory<UserInformation>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<PercentEncoding> percentEncodingLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<Unreserved> unreservedLexer;

        public UserInformationLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
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
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
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
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.unreservedLexer = unreservedLexer;
            this.percentEncodingLexer = percentEncodingLexer;
            this.subcomponentsDelimiterLexer = subcomponentsDelimiterLexer;
        }

        public ILexer<UserInformation> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(
                alternationLexerFactory.Create(
                    unreservedLexer,
                    percentEncodingLexer,
                    subcomponentsDelimiterLexer,
                    terminalLexerFactory.Create(@":", StringComparer.Ordinal)),
                0,
                int.MaxValue);
            return new UserInformationLexer(innerLexer);
        }
    }
}
