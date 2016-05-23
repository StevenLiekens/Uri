using Txt.ABNF;

namespace Uri.IPv6address
{
    public class IPv6Address : Alternation
    {
        public IPv6Address(Alternation alternation)
            : base(alternation)
        {
        }
    }
}
