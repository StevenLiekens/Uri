using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.fragment;
using UriSyntax.hier_part;
using UriSyntax.query;
using UriSyntax.scheme;

namespace UriSyntax.URI
{
    public class UniformResourceIdentifierLexerFactory : LexerFactory<UniformResourceIdentifier>
    {
        static UniformResourceIdentifierLexerFactory()
        {
            Default = new UniformResourceIdentifierLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.OptionLexerFactory.Default,
                scheme.SchemeLexerFactory.Default,
                hier_part.HierarchicalPartLexerFactory.Default,
                query.QueryLexerFactory.Default,
                fragment.FragmentLexerFactory.Default);
        }

        public UniformResourceIdentifierLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexerFactory<Scheme> schemeLexerFactory,
            [NotNull] ILexerFactory<HierarchicalPart> hierarchicalPartLexerFactory,
            [NotNull] ILexerFactory<Query> queryLexerFactory,
            [NotNull] ILexerFactory<Fragment> fragmentLexerFactory)
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
            if (schemeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(schemeLexerFactory));
            }
            if (hierarchicalPartLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hierarchicalPartLexerFactory));
            }
            if (queryLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(queryLexerFactory));
            }
            if (fragmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(fragmentLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            OptionLexerFactory = optionLexerFactory;
            SchemeLexerFactory = schemeLexerFactory.Singleton();
            HierarchicalPartLexerFactory = hierarchicalPartLexerFactory.Singleton();
            QueryLexerFactory = queryLexerFactory.Singleton();
            FragmentLexerFactory = fragmentLexerFactory.Singleton();
        }

        public static UniformResourceIdentifierLexerFactory Default { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<Fragment> FragmentLexerFactory { get; }

        public ILexerFactory<HierarchicalPart> HierarchicalPartLexerFactory { get; }

        public IOptionLexerFactory OptionLexerFactory { get; }

        public ILexerFactory<Query> QueryLexerFactory { get; }

        public ILexerFactory<Scheme> SchemeLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<UniformResourceIdentifier> Create()
        {
            var innerLexer = ConcatenationLexerFactory.Create(
                SchemeLexerFactory.Create(),
                TerminalLexerFactory.Create(@":", StringComparer.Ordinal),
                HierarchicalPartLexerFactory.Create(),
                OptionLexerFactory.Create(
                    ConcatenationLexerFactory.Create(
                        TerminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                        QueryLexerFactory.Create())),
                OptionLexerFactory.Create(
                    ConcatenationLexerFactory.Create(
                        TerminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                        FragmentLexerFactory.Create())));
            return new UniformResourceIdentifierLexer(innerLexer);
        }
    }
}
