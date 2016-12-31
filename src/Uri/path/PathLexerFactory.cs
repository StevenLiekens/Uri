using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.path_abempty;
using UriSyntax.path_absolute;
using UriSyntax.path_empty;
using UriSyntax.path_noscheme;
using UriSyntax.path_rootless;

namespace UriSyntax.path
{
    public class PathLexerFactory : RuleLexerFactory<Path>
    {
        static PathLexerFactory()
        {
            Default = new PathLexerFactory(
                path_abempty.PathAbsoluteOrEmptyLexerFactory.Default.Singleton(),
                path_absolute.PathAbsoluteLexerFactory.Default.Singleton(),
                path_noscheme.PathNoSchemeLexerFactory.Default.Singleton(),
                path_rootless.PathRootlessLexerFactory.Default.Singleton(),
                path_empty.PathEmptyLexerFactory.Default.Singleton());
        }

        public PathLexerFactory(
            [NotNull] ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            [NotNull] ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            [NotNull] ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory,
            [NotNull] ILexerFactory<PathRootless> pathRootlessLexerFactory,
            [NotNull] ILexerFactory<PathEmpty> pathEmptyLexerFactory)
        {
            if (pathAbsoluteOrEmptyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteOrEmptyLexerFactory));
            }
            if (pathAbsoluteLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteLexerFactory));
            }
            if (pathNoSchemeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathNoSchemeLexerFactory));
            }
            if (pathRootlessLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathRootlessLexerFactory));
            }
            if (pathEmptyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathEmptyLexerFactory));
            }
            PathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            PathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            PathNoSchemeLexerFactory = pathNoSchemeLexerFactory;
            PathRootlessLexerFactory = pathRootlessLexerFactory;
            PathEmptyLexerFactory = pathEmptyLexerFactory;
        }

        [NotNull]
        public static PathLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<PathAbsolute> PathAbsoluteLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathAbsoluteOrEmpty> PathAbsoluteOrEmptyLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathEmpty> PathEmptyLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathNoScheme> PathNoSchemeLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathRootless> PathRootlessLexerFactory { get; }

        public override ILexer<Path> Create()
        {
            var innerLexer = Alternation.Create(
                PathAbsoluteOrEmptyLexerFactory.Create(),
                PathAbsoluteLexerFactory.Create(),
                PathNoSchemeLexerFactory.Create(),
                PathRootlessLexerFactory.Create(),
                PathEmptyLexerFactory.Create());
            return new PathLexer(innerLexer);
        }
    }
}
