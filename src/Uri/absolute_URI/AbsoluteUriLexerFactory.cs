using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.hier_part;
using UriSyntax.query;
using UriSyntax.scheme;

namespace UriSyntax.absolute_URI
{
    public class AbsoluteUriLexerFactory : LexerFactory<AbsoluteUri>
    {
        static AbsoluteUriLexerFactory()
        {
            Default = new AbsoluteUriLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.OptionLexerFactory.Default,
                scheme.SchemeLexerFactory.Default.Singleton(),
                hier_part.HierarchicalPartLexerFactory.Default.Singleton(),
                query.QueryLexerFactory.Default.Singleton());
        }

        public AbsoluteUriLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexerFactory<Scheme> schemeLexerFactory,
            [NotNull] ILexerFactory<HierarchicalPart> hierarchicalPartLexerFactory,
            [NotNull] ILexerFactory<Query> queryLexerFactory)
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
            TerminalLexerFactory = terminalLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            OptionLexerFactory = optionLexerFactory;
            SchemeLexerFactory = schemeLexerFactory;
            HierarchicalPartLexerFactory = hierarchicalPartLexerFactory;
            QueryLexerFactory = queryLexerFactory;
        }

        public static AbsoluteUriLexerFactory Default { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<HierarchicalPart> HierarchicalPartLexerFactory { get; }

        public IOptionLexerFactory OptionLexerFactory { get; }

        public ILexerFactory<Query> QueryLexerFactory { get; }

        public ILexerFactory<Scheme> SchemeLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<AbsoluteUri> Create()
        {
            var colon = TerminalLexerFactory.Create(@":", StringComparer.Ordinal);
            var qm = TerminalLexerFactory.Create(@"?", StringComparer.Ordinal);
            var queryPart = ConcatenationLexerFactory.Create(qm, QueryLexerFactory.Create());
            var optQuery = OptionLexerFactory.Create(queryPart);
            var innerLexer = ConcatenationLexerFactory.Create(
                SchemeLexerFactory.Create(),
                colon,
                HierarchicalPartLexerFactory.Create(),
                optQuery);
            return new AbsoluteUriLexer(innerLexer);
        }
    }
}
