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
    public class RelativePartLexerFactory : LexerFactory<RelativePart>
    {
        static RelativePartLexerFactory()
        {
            Default = new RelativePartLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                authority.AuthorityLexerFactory.Default.Singleton(),
                path_abempty.PathAbsoluteOrEmptyLexerFactory.Default.Singleton(),
                path_absolute.PathAbsoluteLexerFactory.Default.Singleton(),
                path_noscheme.PathNoSchemeLexerFactory.Default.Singleton(),
                path_empty.PathEmptyLexerFactory.Default.Singleton());
        }

        public RelativePartLexerFactory(
            ITerminalLexerFactory terminalLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            ILexerFactory<Authority> authorityLexerFactory,
            ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory,
            ILexerFactory<PathEmpty> pathEmptyLexerFactory)
        {
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            AuthorityLexerFactory = authorityLexerFactory;
            PathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            PathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            PathNoSchemeLexerFactory = pathNoSchemeLexerFactory;
            PathEmptyLexerFactory = pathEmptyLexerFactory;
        }

        public static RelativePartLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public ILexerFactory<Authority> AuthorityLexerFactory { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<PathAbsolute> PathAbsoluteLexerFactory { get; }

        public ILexerFactory<PathAbsoluteOrEmpty> PathAbsoluteOrEmptyLexerFactory { get; }

        public ILexerFactory<PathEmpty> PathEmptyLexerFactory { get; }

        public ILexerFactory<PathNoScheme> PathNoSchemeLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<RelativePart> Create()
        {
            var innerLexer =
                AlternationLexerFactory.Create(
                    ConcatenationLexerFactory.Create(
                        TerminalLexerFactory.Create(@"//", StringComparer.Ordinal),
                        AuthorityLexerFactory.Create(),
                        PathAbsoluteOrEmptyLexerFactory.Create()),
                    PathAbsoluteLexerFactory.Create(),
                    PathNoSchemeLexerFactory.Create(),
                    PathEmptyLexerFactory.Create());
            return new RelativePartLexer(innerLexer);
        }
    }
}
