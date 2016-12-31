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
    public class UniformResourceIdentifierLexerFactory : RuleLexerFactory<UniformResourceIdentifier>
    {
        static UniformResourceIdentifierLexerFactory()
        {
            Default = new UniformResourceIdentifierLexerFactory(
                scheme.SchemeLexerFactory.Default.Singleton(),
                hier_part.HierarchicalPartLexerFactory.Default.Singleton(),
                query.QueryLexerFactory.Default.Singleton(),
                fragment.FragmentLexerFactory.Default.Singleton());
        }

        public UniformResourceIdentifierLexerFactory(
            [NotNull] ILexerFactory<Scheme> schemeLexerFactory,
            [NotNull] ILexerFactory<HierarchicalPart> hierarchicalPartLexerFactory,
            [NotNull] ILexerFactory<Query> queryLexerFactory,
            [NotNull] ILexerFactory<Fragment> fragmentLexerFactory)
        {
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
            SchemeLexerFactory = schemeLexerFactory;
            HierarchicalPartLexerFactory = hierarchicalPartLexerFactory;
            QueryLexerFactory = queryLexerFactory;
            FragmentLexerFactory = fragmentLexerFactory;
        }

        [NotNull]
        public static UniformResourceIdentifierLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Fragment> FragmentLexerFactory { get; }

        [NotNull]
        public ILexerFactory<HierarchicalPart> HierarchicalPartLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Query> QueryLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Scheme> SchemeLexerFactory { get; }

        public override ILexer<UniformResourceIdentifier> Create()
        {
            var innerLexer = Concatenation.Create(
                SchemeLexerFactory.Create(),
                Terminal.Create(@":", StringComparer.Ordinal),
                HierarchicalPartLexerFactory.Create(),
                Option.Create(
                    Concatenation.Create(
                        Terminal.Create(@"?", StringComparer.Ordinal),
                        QueryLexerFactory.Create())),
                Option.Create(
                    Concatenation.Create(
                        Terminal.Create(@"#", StringComparer.Ordinal),
                        FragmentLexerFactory.Create())));
            return new UniformResourceIdentifierLexer(innerLexer);
        }
    }
}
