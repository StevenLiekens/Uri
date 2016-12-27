using System;
using JetBrains.Annotations;
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
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexerFactory<Authority> authorityLexerFactory,
            [NotNull] ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            [NotNull] ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            [NotNull] ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory,
            [NotNull] ILexerFactory<PathEmpty> pathEmptyLexerFactory)
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

        [NotNull]
        public static RelativePartLexerFactory Default { get; }

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
        public ILexerFactory<PathNoScheme> PathNoSchemeLexerFactory { get; }

        [NotNull]
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
