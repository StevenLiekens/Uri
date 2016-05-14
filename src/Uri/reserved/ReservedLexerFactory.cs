using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Uri.gen_delims;
using Uri.sub_delims;

namespace Uri.reserved
{
    public class ReservedLexerFactory : ILexerFactory<Reserved>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<GenericDelimiter> genericDelimiterLexer;

        private readonly ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer;

        public ReservedLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<GenericDelimiter> genericDelimiterLexer,
            [NotNull] ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (genericDelimiterLexer == null)
            {
                throw new ArgumentNullException(nameof(genericDelimiterLexer));
            }
            if (subcomponentsDelimiterLexer == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.genericDelimiterLexer = genericDelimiterLexer;
            this.subcomponentsDelimiterLexer = subcomponentsDelimiterLexer;
        }

        public ILexer<Reserved> Create()
        {
            var innerLexer = alternationLexerFactory.Create(
                genericDelimiterLexer,
                subcomponentsDelimiterLexer);
            return new ReservedLexer(innerLexer);
        }
    }
}
