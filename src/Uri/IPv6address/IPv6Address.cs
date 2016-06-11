using Txt.ABNF;

namespace UriSyntax.IPv6address
{
    public class IPv6Address : Alternation
    {
        public IPv6Address(Alternation alternation)
            : base(alternation)
        {
        }
    }
}
