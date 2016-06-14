using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.IPv6address
{
    public sealed class IPv6AddressLexer : CompositeLexer<Alternation, IPv6Address>
    {
        public IPv6AddressLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
