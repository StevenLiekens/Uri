using System;
using Txt;
using Txt.ABNF;
using Uri.path_abempty;
using Uri.path_absolute;
using Uri.path_empty;
using Uri.path_noscheme;
using Uri.path_rootless;

namespace Uri.path
{
    public class PathLexerFactory : ILexerFactory<Path>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory;

        private readonly ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory;

        private readonly ILexerFactory<PathEmpty> pathEmptyLexerFactory;

        private readonly ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory;

        private readonly ILexerFactory<PathRootless> pathRootlessLexerFactory;

        public PathLexerFactory(
            IAlternationLexerFactory alternationLexerFactory,
            ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory,
            ILexerFactory<PathRootless> pathRootlessLexerFactory,
            ILexerFactory<PathEmpty> pathEmptyLexerFactory)
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

            this.alternationLexerFactory = alternationLexerFactory;
            this.pathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            this.pathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            this.pathNoSchemeLexerFactory = pathNoSchemeLexerFactory;
            this.pathRootlessLexerFactory = pathRootlessLexerFactory;
            this.pathEmptyLexerFactory = pathEmptyLexerFactory;
        }

        public ILexer<Path> Create()
        {
            ILexer[] a =
                {
                    pathAbsoluteOrEmptyLexerFactory.Create(), pathAbsoluteLexerFactory.Create(),
                    pathNoSchemeLexerFactory.Create(), pathRootlessLexerFactory.Create(),
                    pathEmptyLexerFactory.Create()
                };

            var b = alternationLexerFactory.Create(a);
            return new PathLexer(b);
        }
    }
}