using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.gen_delims;
using UriSyntax.sub_delims;

namespace UriSyntax.reserved
{
    public class ReservedLexerFactory : LexerFactory<Reserved>
    {
        static ReservedLexerFactory()
        {
            Default = new ReservedLexerFactory(
                Txt.ABNF.AlternationLexerFactory.Default,
                gen_delims.GenericDelimiterLexerFactory.Default,
                sub_delims.SubcomponentsDelimiterLexerFactory.Default);
        }

        public ReservedLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexerFactory<GenericDelimiter> genericDelimiterLexerFactory,
            [NotNull] ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (genericDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(genericDelimiterLexerFactory));
            }
            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }
            AlternationLexerFactory = alternationLexerFactory;
            GenericDelimiterLexerFactory = genericDelimiterLexerFactory.Singleton();
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory.Singleton();
        }

        public static ReservedLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public ILexerFactory<GenericDelimiter> GenericDelimiterLexerFactory { get; }

        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        public override ILexer<Reserved> Create()
        {
            var innerLexer = AlternationLexerFactory.Create(
                GenericDelimiterLexerFactory.Create(),
                SubcomponentsDelimiterLexerFactory.Create());
            return new ReservedLexer(innerLexer);
        }
    }
}
