using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.hier_part;
using UriSyntax.query;
using UriSyntax.scheme;

namespace UriSyntax.absolute_URI
{
    public class AbsoluteUriLexerFactory : RuleLexerFactory<AbsoluteUri>
    {
        static AbsoluteUriLexerFactory()
        {
            Default = new AbsoluteUriLexerFactory(
                scheme.SchemeLexerFactory.Default.Singleton(),
                hier_part.HierarchicalPartLexerFactory.Default.Singleton(),
                query.QueryLexerFactory.Default.Singleton());
        }

        public AbsoluteUriLexerFactory(
            [NotNull] ILexerFactory<Scheme> schemeLexerFactory,
            [NotNull] ILexerFactory<HierarchicalPart> hierarchicalPartLexerFactory,
            [NotNull] ILexerFactory<Query> queryLexerFactory)
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
            SchemeLexerFactory = schemeLexerFactory;
            HierarchicalPartLexerFactory = hierarchicalPartLexerFactory;
            QueryLexerFactory = queryLexerFactory;
        }

        [NotNull]
        public static AbsoluteUriLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HierarchicalPart> HierarchicalPartLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Query> QueryLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Scheme> SchemeLexerFactory { get; }

        public override ILexer<AbsoluteUri> Create()
        {
            var colon = Terminal.Create(@":", StringComparer.Ordinal);
            var qm = Terminal.Create(@"?", StringComparer.Ordinal);
            var queryPart = Concatenation.Create(qm, QueryLexerFactory.Create());
            var optQuery = Option.Create(queryPart);
            var innerLexer = Concatenation.Create(
                SchemeLexerFactory.Create(),
                colon,
                HierarchicalPartLexerFactory.Create(),
                optQuery);
            return new AbsoluteUriLexer(innerLexer);
        }
    }
}
