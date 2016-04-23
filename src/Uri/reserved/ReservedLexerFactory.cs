using System;
using Txt;
using Txt.ABNF;
using Uri.gen_delims;
using Uri.sub_delims;

namespace Uri.reserved
{
    public class ReservedLexerFactory : ILexerFactory<Reserved>
    {
        private readonly ILexerFactory<GenericDelimiter> genericDelimiterLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        public ReservedLexerFactory(ILexerFactory<GenericDelimiter> genericDelimiterLexerFactory, ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory, IAlternationLexerFactory alternationLexerFactory)
        {
            if (genericDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(genericDelimiterLexerFactory));
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }

            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }

            this.genericDelimiterLexerFactory = genericDelimiterLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
        }

        public ILexer<Reserved> Create()
        {
            var reservedAlterativeLexer = alternationLexerFactory.Create(
                genericDelimiterLexerFactory.Create(),
                subcomponentsDelimiterLexerFactory.Create());
            return new ReservedLexer(reservedAlterativeLexer);
        }
    }
}
