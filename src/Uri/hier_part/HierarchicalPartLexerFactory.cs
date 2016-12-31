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
    public class HierarchicalPartLexerFactory : RuleLexerFactory<HierarchicalPart>
    {
        static HierarchicalPartLexerFactory()
        {
            Default = new HierarchicalPartLexerFactory(
                authority.AuthorityLexerFactory.Default.Singleton(),
                path_abempty.PathAbsoluteOrEmptyLexerFactory.Default.Singleton(),
                path_absolute.PathAbsoluteLexerFactory.Default.Singleton(),
                path_rootless.PathRootlessLexerFactory.Default.Singleton(),
                path_empty.PathEmptyLexerFactory.Default.Singleton());
        }

        public HierarchicalPartLexerFactory(
            [NotNull] ILexerFactory<Authority> authorityLexerFactory,
            [NotNull] ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            [NotNull] ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            [NotNull] ILexerFactory<PathRootless> pathRootlessLexerFactory,
            [NotNull] ILexerFactory<PathEmpty> pathEmptyLexerFactory)
        {
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
            AuthorityLexerFactory = authorityLexerFactory;
            PathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            PathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            PathRootlessLexerFactory = pathRootlessLexerFactory;
            PathEmptyLexerFactory = pathEmptyLexerFactory;
        }

        [NotNull]
        public static HierarchicalPartLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Authority> AuthorityLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathAbsolute> PathAbsoluteLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathAbsoluteOrEmpty> PathAbsoluteOrEmptyLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathEmpty> PathEmptyLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathRootless> PathRootlessLexerFactory { get; }

        public override ILexer<HierarchicalPart> Create()
        {
            var delim = Terminal.Create(@"//", StringComparer.Ordinal);
            var seq = Concatenation.Create(
                delim,
                AuthorityLexerFactory.Create(),
                PathAbsoluteOrEmptyLexerFactory.Create());
            var innerLexer = Alternation.Create(
                seq,
                PathAbsoluteLexerFactory.Create(),
                PathRootlessLexerFactory.Create(),
                PathEmptyLexerFactory.Create());
            return new HierarchicalPartLexer(innerLexer);
        }
    }
}
