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
    public class PathLexerFactory : ILexerFactory<Path>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<PathAbsolute> pathAbsoluteLexer;

        private readonly ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer;

        private readonly ILexer<PathEmpty> pathEmptyLexer;

        private readonly ILexer<PathNoScheme> pathNoSchemeLexer;

        private readonly ILexer<PathRootless> pathRootlessLexer;

        public PathLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer,
            [NotNull] ILexer<PathAbsolute> pathAbsoluteLexer,
            [NotNull] ILexer<PathNoScheme> pathNoSchemeLexer,
            [NotNull] ILexer<PathRootless> pathRootlessLexer,
            [NotNull] ILexer<PathEmpty> pathEmptyLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (pathAbsoluteOrEmptyLexer == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteOrEmptyLexer));
            }
            if (pathAbsoluteLexer == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteLexer));
            }
            if (pathNoSchemeLexer == null)
            {
                throw new ArgumentNullException(nameof(pathNoSchemeLexer));
            }
            if (pathRootlessLexer == null)
            {
                throw new ArgumentNullException(nameof(pathRootlessLexer));
            }
            if (pathEmptyLexer == null)
            {
                throw new ArgumentNullException(nameof(pathEmptyLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.pathAbsoluteOrEmptyLexer = pathAbsoluteOrEmptyLexer;
            this.pathAbsoluteLexer = pathAbsoluteLexer;
            this.pathNoSchemeLexer = pathNoSchemeLexer;
            this.pathRootlessLexer = pathRootlessLexer;
            this.pathEmptyLexer = pathEmptyLexer;
        }

        public ILexer<Path> Create()
        {
            var innerLexer = alternationLexerFactory.Create(
                pathAbsoluteOrEmptyLexer,
                pathAbsoluteLexer,
                pathNoSchemeLexer,
                pathRootlessLexer,
                pathEmptyLexer);
            return new PathLexer(innerLexer);
        }
    }
}
