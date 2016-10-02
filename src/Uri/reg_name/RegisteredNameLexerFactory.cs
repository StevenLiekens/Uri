using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pct_encoded;
using UriSyntax.sub_delims;
using UriSyntax.unreserved;

namespace UriSyntax.reg_name
{
    public class RegisteredNameLexerFactory : LexerFactory<RegisteredName>
    {
        static RegisteredNameLexerFactory()
        {
            Default = new RegisteredNameLexerFactory(
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                unreserved.UnreservedLexerFactory.Default,
                pct_encoded.PercentEncodingLexerFactory.Default,
                sub_delims.SubcomponentsDelimiterLexerFactory.Default);
        }

        public RegisteredNameLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexerFactory<Unreserved> unreservedLexerFactory,
            [NotNull] ILexerFactory<PercentEncoding> percentEncodingLexerFactory,
            [NotNull] ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
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
            AlternationLexerFactory = alternationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            UnreservedLexerFactory = unreservedLexerFactory.Singleton();
            PercentEncodingLexerFactory = percentEncodingLexerFactory.Singleton();
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory.Singleton();
        }

        public static RegisteredNameLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public ILexerFactory<PercentEncoding> PercentEncodingLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        public ILexerFactory<Unreserved> UnreservedLexerFactory { get; }

        public override ILexer<RegisteredName> Create()
        {
            var innerLexer =
                RepetitionLexerFactory.Create(
                    AlternationLexerFactory.Create(
                        UnreservedLexerFactory.Create(),
                        PercentEncodingLexerFactory.Create(),
                        SubcomponentsDelimiterLexerFactory.Create()),
                    0,
                    int.MaxValue);
            return new RegisteredNameLexer(innerLexer);
        }
    }
}
