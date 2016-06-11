using System;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.authority;
using UriSyntax.path_abempty;
using UriSyntax.path_absolute;
using UriSyntax.path_empty;
using UriSyntax.path_noscheme;

namespace UriSyntax.relative_part
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
