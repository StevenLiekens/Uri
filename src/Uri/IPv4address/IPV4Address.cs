using Txt.ABNF;

namespace UriSyntax.IPv4address
{
    public class IPv4Address : Concatenation
    {
        public IPv4Address(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
