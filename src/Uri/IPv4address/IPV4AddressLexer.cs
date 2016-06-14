using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.IPv4address
{
    public sealed class IPv4AddressLexer : CompositeLexer<Concatenation, IPv4Address>
    {
        public IPv4AddressLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
