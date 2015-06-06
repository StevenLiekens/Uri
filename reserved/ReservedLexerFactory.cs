﻿namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class ReservedLexerFactory : ILexerFactory<Reserved>
    {
        private readonly ILexerFactory<GenericDelimiter> genericDelimiterLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        public ReservedLexerFactory(ILexerFactory<GenericDelimiter> genericDelimiterLexerFactory, ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory, IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (genericDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException("genericDelimiterLexerFactory", "Precondition: genericDelimiterLexerFactory != null");
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException("subcomponentsDelimiterLexerFactory", "Precondition: subcomponentsDelimiterLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory", "Precondition: alternativeLexerFactory != null");
            }

            this.genericDelimiterLexerFactory = genericDelimiterLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<Reserved> Create()
        {
            var reservedAlterativeLexer = this.alternativeLexerFactory.Create(
                this.genericDelimiterLexerFactory.Create(),
                this.subcomponentsDelimiterLexerFactory.Create());
            return new ReservedLexer(reservedAlterativeLexer);
        }
    }
}
