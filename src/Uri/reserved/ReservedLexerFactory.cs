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

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        public ReservedLexerFactory(ILexerFactory<GenericDelimiter> genericDelimiterLexerFactory, ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory, IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (genericDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(genericDelimiterLexerFactory));
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            this.genericDelimiterLexerFactory = genericDelimiterLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<Reserved> Create()
        {
            var reservedAlterativeLexer = alternativeLexerFactory.Create(
                genericDelimiterLexerFactory.Create(),
                subcomponentsDelimiterLexerFactory.Create());
            return new ReservedLexer(reservedAlterativeLexer);
        }
    }
}
