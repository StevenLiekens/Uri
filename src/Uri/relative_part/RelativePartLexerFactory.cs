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
    public class RelativePartLexerFactory : RuleLexerFactory<RelativePart>
    {
        static RelativePartLexerFactory()
        {
            Default = new RelativePartLexerFactory(
                authority.AuthorityLexerFactory.Default.Singleton(),
                path_abempty.PathAbsoluteOrEmptyLexerFactory.Default.Singleton(),
                path_absolute.PathAbsoluteLexerFactory.Default.Singleton(),
                path_noscheme.PathNoSchemeLexerFactory.Default.Singleton(),
                path_empty.PathEmptyLexerFactory.Default.Singleton());
        }

        public RelativePartLexerFactory(
            [NotNull] ILexerFactory<Authority> authorityLexerFactory,
            [NotNull] ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            [NotNull] ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            [NotNull] ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory,
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
            if (pathNoSchemeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathNoSchemeLexerFactory));
            }
            if (pathEmptyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathEmptyLexerFactory));
            }
            AuthorityLexerFactory = authorityLexerFactory;
            PathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            PathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            PathNoSchemeLexerFactory = pathNoSchemeLexerFactory;
            PathEmptyLexerFactory = pathEmptyLexerFactory;
        }

        [NotNull]
        public static RelativePartLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Authority> AuthorityLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathAbsolute> PathAbsoluteLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathAbsoluteOrEmpty> PathAbsoluteOrEmptyLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathEmpty> PathEmptyLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathNoScheme> PathNoSchemeLexerFactory { get; }

        public override ILexer<RelativePart> Create()
        {
            var innerLexer =
                Alternation.Create(
                    Concatenation.Create(
                        Terminal.Create(@"//", StringComparer.Ordinal),
                        AuthorityLexerFactory.Create(),
                        PathAbsoluteOrEmptyLexerFactory.Create()),
                    PathAbsoluteLexerFactory.Create(),
                    PathNoSchemeLexerFactory.Create(),
                    PathEmptyLexerFactory.Create());
            return new RelativePartLexer(innerLexer);
        }
    }
}
