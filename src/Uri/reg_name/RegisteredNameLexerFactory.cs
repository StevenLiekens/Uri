using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pct_encoded;
using UriSyntax.sub_delims;
using UriSyntax.unreserved;

namespace UriSyntax.reg_name
{
    public class RegisteredNameLexerFactory : RuleLexerFactory<RegisteredName>
    {
        static RegisteredNameLexerFactory()
        {
            Default = new RegisteredNameLexerFactory(
                unreserved.UnreservedLexerFactory.Default.Singleton(),
                pct_encoded.PercentEncodingLexerFactory.Default.Singleton(),
                sub_delims.SubcomponentsDelimiterLexerFactory.Default.Singleton());
        }

        public RegisteredNameLexerFactory(
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
        public static RegisteredNameLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<PercentEncoding> PercentEncodingLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Unreserved> UnreservedLexerFactory { get; }

        public override ILexer<RegisteredName> Create()
        {
            var innerLexer =
                Repetition.Create(
                    Alternation.Create(
                        UnreservedLexerFactory.Create(),
                        PercentEncodingLexerFactory.Create(),
                        SubcomponentsDelimiterLexerFactory.Create()),
                    0,
                    int.MaxValue);
            return new RegisteredNameLexer(innerLexer);
        }
    }
}
