using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.host
{
    public sealed class HostLexer : CompositeLexer<Alternation, Host>
    {
        public HostLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
