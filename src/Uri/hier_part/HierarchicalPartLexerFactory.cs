using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.authority;
using UriSyntax.path_abempty;
using UriSyntax.path_absolute;
using UriSyntax.path_empty;
using UriSyntax.path_rootless;

namespace UriSyntax.hier_part
{
    public class HierarchicalPartLexerFactory : LexerFactory<HierarchicalPart>
    {
        static HierarchicalPartLexerFactory()
        {
            Default = new HierarchicalPartLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                authority.AuthorityLexerFactory.Default.Singleton(),
                path_abempty.PathAbsoluteOrEmptyLexerFactory.Default.Singleton(),
                path_absolute.PathAbsoluteLexerFactory.Default.Singleton(),
                path_rootless.PathRootlessLexerFactory.Default.Singleton(),
                path_empty.PathEmptyLexerFactory.Default.Singleton());
        }

        public HierarchicalPartLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexerFactory<Authority> authorityLexerFactory,
            [NotNull] ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            [NotNull] ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            [NotNull] ILexerFactory<PathRootless> pathRootlessLexerFactory,
            [NotNull] ILexerFactory<PathEmpty> pathEmptyLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (authorityLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(authorityLexerFactory));
            }
            if (pathAbsoluteOrEmptyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteOrEmptyLexerFactory));
            }
            if (pathAbsoluteLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteLexerFactory));
            }
            if (pathRootlessLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathRootlessLexerFactory));
            }
            if (pathEmptyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathEmptyLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            AuthorityLexerFactory = authorityLexerFactory;
            PathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            PathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            PathRootlessLexerFactory = pathRootlessLexerFactory;
            PathEmptyLexerFactory = pathEmptyLexerFactory;
        }

        [NotNull]
        public static HierarchicalPartLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Authority> AuthorityLexerFactory { get; }

        [NotNull]
        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathAbsolute> PathAbsoluteLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathAbsoluteOrEmpty> PathAbsoluteOrEmptyLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathEmpty> PathEmptyLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathRootless> PathRootlessLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<HierarchicalPart> Create()
        {
            var delim = TerminalLexerFactory.Create(@"//", StringComparer.Ordinal);
            var seq = ConcatenationLexerFactory.Create(
                delim,
                AuthorityLexerFactory.Create(),
                PathAbsoluteOrEmptyLexerFactory.Create());
            var innerLexer = AlternationLexerFactory.Create(
                seq,
                PathAbsoluteLexerFactory.Create(),
                PathRootlessLexerFactory.Create(),
                PathEmptyLexerFactory.Create());
            return new HierarchicalPartLexer(innerLexer);
        }
    }
}
