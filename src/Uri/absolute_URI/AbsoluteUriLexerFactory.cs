using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Uri.hier_part;
using Uri.query;
using Uri.scheme;

namespace Uri.absolute_URI
{
    public class AbsoluteUriLexerFactory : ILexerFactory<AbsoluteUri>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HierarchicalPart> hierarchicalPartLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<Query> queryLexer;

        private readonly ILexer<Scheme> schemeLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public AbsoluteUriLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<Scheme> schemeLexer,
            [NotNull] ILexer<HierarchicalPart> hierarchicalPartLexer,
            [NotNull] ILexer<Query> queryLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (schemeLexer == null)
            {
                throw new ArgumentNullException(nameof(schemeLexer));
            }
            if (hierarchicalPartLexer == null)
            {
                throw new ArgumentNullException(nameof(hierarchicalPartLexer));
            }
            if (queryLexer == null)
            {
                throw new ArgumentNullException(nameof(queryLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.schemeLexer = schemeLexer;
            this.hierarchicalPartLexer = hierarchicalPartLexer;
            this.queryLexer = queryLexer;
        }

        public ILexer<AbsoluteUri> Create()
        {
            var colon = terminalLexerFactory.Create(@":", StringComparer.Ordinal);
            var qm = terminalLexerFactory.Create(@"?", StringComparer.Ordinal);
            var queryPart = concatenationLexerFactory.Create(qm, queryLexer);
            var optQuery = optionLexerFactory.Create(queryPart);
            var innerLexer = concatenationLexerFactory.Create(schemeLexer, colon, hierarchicalPartLexer, optQuery);
            return new AbsoluteUriLexer(innerLexer);
        }
    }
}
