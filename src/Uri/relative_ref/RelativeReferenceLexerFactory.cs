using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.fragment;
using UriSyntax.query;
using UriSyntax.relative_part;

namespace UriSyntax.relative_ref
{
    public sealed class RelativeReferenceLexerFactory : RuleLexerFactory<RelativeReference>
    {
        static RelativeReferenceLexerFactory()
        {
            Default = new RelativeReferenceLexerFactory(
                relative_part.RelativePartLexerFactory.Default.Singleton(),
                query.QueryLexerFactory.Default.Singleton(),
                fragment.FragmentLexerFactory.Default.Singleton());
        }

        public RelativeReferenceLexerFactory(
            [NotNull] ILexerFactory<RelativePart> relativePartLexerFactory,
            [NotNull] ILexerFactory<Query> queryLexerFactory,
            [NotNull] ILexerFactory<Fragment> fragmentLexerFactory)
        {
            if (relativePartLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(relativePartLexerFactory));
            }
            if (queryLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(queryLexerFactory));
            }
            if (fragmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(fragmentLexerFactory));
            }
            RelativePartLexerFactory = relativePartLexerFactory;
            QueryLexerFactory = queryLexerFactory;
            FragmentLexerFactory = fragmentLexerFactory;
        }

        [NotNull]
        public static RelativeReferenceLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Fragment> FragmentLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Query> QueryLexerFactory { get; }

        [NotNull]
        public ILexerFactory<RelativePart> RelativePartLexerFactory { get; }

        public override ILexer<RelativeReference> Create()
        {
            var innerLexer = Concatenation.Create(
                RelativePartLexerFactory.Create(),
                Option.Create(
                    Concatenation.Create(
                        Terminal.Create(@"?", StringComparer.Ordinal),
                        QueryLexerFactory.Create())),
                Option.Create(
                    Concatenation.Create(
                        Terminal.Create(@"#", StringComparer.Ordinal),
                        FragmentLexerFactory.Create())));
            return new RelativeReferenceLexer(innerLexer);
        }
    }
}
