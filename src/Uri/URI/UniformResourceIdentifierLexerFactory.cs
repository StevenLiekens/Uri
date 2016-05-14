using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Uri.fragment;
using Uri.hier_part;
using Uri.query;
using Uri.scheme;

namespace Uri.URI
{
    public class UniformResourceIdentifierLexerFactory : ILexerFactory<UniformResourceIdentifier>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Fragment> fragmentLexer;

        private readonly ILexer<HierarchicalPart> hierarchicalPartLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<Query> queryLexer;

        private readonly ILexer<Scheme> schemeLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public UniformResourceIdentifierLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<Scheme> schemeLexer,
            [NotNull] ILexer<HierarchicalPart> hierarchicalPartLexer,
            [NotNull] ILexer<Query> queryLexer,
            [NotNull] ILexer<Fragment> fragmentLexer)
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
            if (fragmentLexer == null)
            {
                throw new ArgumentNullException(nameof(fragmentLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.schemeLexer = schemeLexer;
            this.hierarchicalPartLexer = hierarchicalPartLexer;
            this.queryLexer = queryLexer;
            this.fragmentLexer = fragmentLexer;
            this.concatenationLexerFactory = concatenationLexerFactory;
        }

        public ILexer<UniformResourceIdentifier> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                schemeLexer,
                terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                hierarchicalPartLexer,
                optionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                        queryLexer)),
                optionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                        fragmentLexer)));
            return new UniformResourceIdentifierLexer(innerLexer);
        }
    }
}
