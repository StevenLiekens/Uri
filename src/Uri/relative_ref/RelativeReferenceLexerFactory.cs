using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.fragment;
using UriSyntax.query;
using UriSyntax.relative_part;

namespace UriSyntax.relative_ref
{
    public class RelativeReferenceLexerFactory : ILexerFactory<RelativeReference>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Fragment> fragmentLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<Query> queryLexer;

        private readonly ILexer<RelativePart> relativePartLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public RelativeReferenceLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<RelativePart> relativePartLexer,
            [NotNull] ILexer<Query> queryLexer,
            [NotNull] ILexer<Fragment> fragmentLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (relativePartLexer == null)
            {
                throw new ArgumentNullException(nameof(relativePartLexer));
            }
            if (queryLexer == null)
            {
                throw new ArgumentNullException(nameof(queryLexer));
            }
            if (fragmentLexer == null)
            {
                throw new ArgumentNullException(nameof(fragmentLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.relativePartLexer = relativePartLexer;
            this.queryLexer = queryLexer;
            this.fragmentLexer = fragmentLexer;
        }

        public ILexer<RelativeReference> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                relativePartLexer,
                optionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                        queryLexer)),
                optionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                        fragmentLexer)));
            return new RelativeReferenceLexer(innerLexer);
        }
    }
}
