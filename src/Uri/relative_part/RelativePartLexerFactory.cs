using System;
using Txt.Core;
using Txt.ABNF;
using Uri.authority;
using Uri.path_abempty;
using Uri.path_absolute;
using Uri.path_empty;
using Uri.path_noscheme;

namespace Uri.relative_part
{
    public class RelativePartLexerFactory : ILexerFactory<RelativePart>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<Authority> authorityLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<PathAbsolute> pathAbsoluteLexer;

        private readonly ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer;

        private readonly ILexer<PathEmpty> pathEmptyLexer;

        private readonly ILexer<PathNoScheme> pathNoSchemeLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public RelativePartLexerFactory(
            ITerminalLexerFactory terminalLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            ILexer<Authority> authorityLexer,
            ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer,
            ILexer<PathAbsolute> pathAbsoluteLexer,
            ILexer<PathNoScheme> pathNoSchemeLexer,
            ILexer<PathEmpty> pathEmptyLexer)
        {
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.authorityLexer = authorityLexer;
            this.pathAbsoluteOrEmptyLexer = pathAbsoluteOrEmptyLexer;
            this.pathAbsoluteLexer = pathAbsoluteLexer;
            this.pathNoSchemeLexer = pathNoSchemeLexer;
            this.pathEmptyLexer = pathEmptyLexer;
        }

        public ILexer<RelativePart> Create()
        {
            var innerLexer =
                alternationLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"//", StringComparer.Ordinal),
                        authorityLexer,
                        pathAbsoluteOrEmptyLexer),
                    pathAbsoluteLexer,
                    pathNoSchemeLexer,
                    pathEmptyLexer);
            return new RelativePartLexer(innerLexer);
        }
    }
}
