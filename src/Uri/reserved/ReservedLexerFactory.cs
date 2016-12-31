using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.gen_delims;
using UriSyntax.sub_delims;

namespace UriSyntax.reserved
{
    public class ReservedLexerFactory : RuleLexerFactory<Reserved>
    {
        static ReservedLexerFactory()
        {
            Default = new ReservedLexerFactory(
                gen_delims.GenericDelimiterLexerFactory.Default.Singleton(),
                sub_delims.SubcomponentsDelimiterLexerFactory.Default.Singleton());
        }

        public ReservedLexerFactory(
            [NotNull] ILexerFactory<GenericDelimiter> genericDelimiterLexerFactory,
            [NotNull] ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
            if (genericDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(genericDelimiterLexerFactory));
            }
            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }
            GenericDelimiterLexerFactory = genericDelimiterLexerFactory;
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
        }

        [NotNull]
        public static ReservedLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<GenericDelimiter> GenericDelimiterLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        public override ILexer<Reserved> Create()
        {
            var innerLexer = Alternation.Create(
                GenericDelimiterLexerFactory.Create(),
                SubcomponentsDelimiterLexerFactory.Create());
            return new ReservedLexer(innerLexer);
        }
    }
}
