using Txt.ABNF;

namespace Uri.IPv4address
{
    public class IPv4Address : Concatenation
    {
        public IPv4Address(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}