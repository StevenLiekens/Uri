using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.fragment;
using UriSyntax.query;
using UriSyntax.relative_part;

namespace UriSyntax.relative_ref
{
    public class RelativeReferenceLexerFactory : LexerFactory<RelativeReference>
    {
        static RelativeReferenceLexerFactory()
        {
            Default = new RelativeReferenceLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.OptionLexerFactory.Default,
                relative_part.RelativePartLexerFactory.Default.Singleton(),
                query.QueryLexerFactory.Default.Singleton(),
                fragment.FragmentLexerFactory.Default.Singleton());
        }

        public RelativeReferenceLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexerFactory<RelativePart> relativePartLexerFactory,
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
            TerminalLexerFactory = terminalLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            OptionLexerFactory = optionLexerFactory;
            RelativePartLexerFactory = relativePartLexerFactory;
            QueryLexerFactory = queryLexerFactory;
            FragmentLexerFactory = fragmentLexerFactory;
        }

        [NotNull]
        public static RelativeReferenceLexerFactory Default { get; }

        [NotNull]
        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Fragment> FragmentLexerFactory { get; }

        [NotNull]
        public IOptionLexerFactory OptionLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Query> QueryLexerFactory { get; }

        [NotNull]
        public ILexerFactory<RelativePart> RelativePartLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<RelativeReference> Create()
        {
            var innerLexer = ConcatenationLexerFactory.Create(
                RelativePartLexerFactory.Create(),
                OptionLexerFactory.Create(
                    ConcatenationLexerFactory.Create(
                        TerminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                        QueryLexerFactory.Create())),
                OptionLexerFactory.Create(
                    ConcatenationLexerFactory.Create(
                        TerminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                        FragmentLexerFactory.Create())));
            return new RelativeReferenceLexer(innerLexer);
        }
    }
}
