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
    public class HierarchicalPartLexerFactory : ILexerFactory<HierarchicalPart>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<Authority> authorityLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<PathAbsolute> pathAbsoluteLexer;

        private readonly ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer;

        private readonly ILexer<PathEmpty> pathEmptyLexer;

        private readonly ILexer<PathRootless> pathRootlessLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HierarchicalPartLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<Authority> authorityLexer,
            [NotNull] ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer,
            [NotNull] ILexer<PathAbsolute> pathAbsoluteLexer,
            [NotNull] ILexer<PathRootless> pathRootlessLexer,
            [NotNull] ILexer<PathEmpty> pathEmptyLexer)
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
            if (authorityLexer == null)
            {
                throw new ArgumentNullException(nameof(authorityLexer));
            }
            if (pathAbsoluteOrEmptyLexer == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteOrEmptyLexer));
            }
            if (pathAbsoluteLexer == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteLexer));
            }
            if (pathRootlessLexer == null)
            {
                throw new ArgumentNullException(nameof(pathRootlessLexer));
            }
            if (pathEmptyLexer == null)
            {
                throw new ArgumentNullException(nameof(pathEmptyLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.authorityLexer = authorityLexer;
            this.pathAbsoluteOrEmptyLexer = pathAbsoluteOrEmptyLexer;
            this.pathAbsoluteLexer = pathAbsoluteLexer;
            this.pathRootlessLexer = pathRootlessLexer;
            this.pathEmptyLexer = pathEmptyLexer;
        }

        public ILexer<HierarchicalPart> Create()
        {
            var delim = terminalLexerFactory.Create(@"//", StringComparer.Ordinal);
            var seq = concatenationLexerFactory.Create(delim, authorityLexer, pathAbsoluteOrEmptyLexer);
            var innerLexer = alternationLexerFactory.Create(seq, pathAbsoluteLexer, pathRootlessLexer, pathEmptyLexer);
            return new HierarchicalPartLexer(innerLexer);
        }
    }
}
