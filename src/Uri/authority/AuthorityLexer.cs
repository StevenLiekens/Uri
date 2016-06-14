using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.authority
{
    public sealed class AuthorityLexer : CompositeLexer<Concatenation, Authority>
    {
        public AuthorityLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
