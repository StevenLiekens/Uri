using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Uri.pct_encoded;
using Uri.sub_delims;
using Uri.unreserved;

namespace Uri.reg_name
{
    public class RegisteredNameLexerFactory : ILexerFactory<RegisteredName>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<PercentEncoding> percentEncodingLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer;

        private readonly ILexer<Unreserved> unreservedLexer;

        public RegisteredNameLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Unreserved> unreservedLexer,
            [NotNull] ILexer<PercentEncoding> percentEncodingLexer,
            [NotNull] ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer)
        {
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
            this.alternationLexerFactory = alternationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.unreservedLexer = unreservedLexer;
            this.percentEncodingLexer = percentEncodingLexer;
            this.subcomponentsDelimiterLexer = subcomponentsDelimiterLexer;
        }

        public ILexer<RegisteredName> Create()
        {
            var innerLexer =
                repetitionLexerFactory.Create(
                    alternationLexerFactory.Create(unreservedLexer, percentEncodingLexer, subcomponentsDelimiterLexer),
                    0,
                    int.MaxValue);
            return new RegisteredNameLexer(innerLexer);
        }
    }
}
