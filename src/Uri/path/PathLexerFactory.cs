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
    public class PathLexerFactory : LexerFactory<Path>
    {
        static PathLexerFactory()
        {
            Default = new PathLexerFactory(
                Txt.ABNF.AlternationLexerFactory.Default,
                path_abempty.PathAbsoluteOrEmptyLexerFactory.Default.Singleton(),
                path_absolute.PathAbsoluteLexerFactory.Default.Singleton(),
                path_noscheme.PathNoSchemeLexerFactory.Default.Singleton(),
                path_rootless.PathRootlessLexerFactory.Default.Singleton(),
                path_empty.PathEmptyLexerFactory.Default.Singleton());
        }

        public PathLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            [NotNull] ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            [NotNull] ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory,
            [NotNull] ILexerFactory<PathRootless> pathRootlessLexerFactory,
            [NotNull] ILexerFactory<PathEmpty> pathEmptyLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
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
            AlternationLexerFactory = alternationLexerFactory;
            PathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            PathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            PathNoSchemeLexerFactory = pathNoSchemeLexerFactory;
            PathRootlessLexerFactory = pathRootlessLexerFactory;
            PathEmptyLexerFactory = pathEmptyLexerFactory;
        }

        public static PathLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public ILexerFactory<PathAbsolute> PathAbsoluteLexerFactory { get; }

        public ILexerFactory<PathAbsoluteOrEmpty> PathAbsoluteOrEmptyLexerFactory { get; }

        public ILexerFactory<PathEmpty> PathEmptyLexerFactory { get; }

        public ILexerFactory<PathNoScheme> PathNoSchemeLexerFactory { get; }

        public ILexerFactory<PathRootless> PathRootlessLexerFactory { get; }

        public override ILexer<Path> Create()
        {
            var innerLexer = AlternationLexerFactory.Create(
                PathAbsoluteOrEmptyLexerFactory.Create(),
                PathAbsoluteLexerFactory.Create(),
                PathNoSchemeLexerFactory.Create(),
                PathRootlessLexerFactory.Create(),
                PathEmptyLexerFactory.Create());
            return new PathLexer(innerLexer);
        }
    }
}
