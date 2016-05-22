using System.Linq;
using Txt.ABNF;
using Uri.dec_octet;

namespace Uri.IPv4address
{
    public class IPv4Address : Concatenation
    {
        public IPv4Address(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public byte[] GetBytes()
        {
            var octets = Elements.OfType<DecimalOctet>();
            return octets.Select(octet => byte.Parse(octet.Text)).ToArray();
        }
    }
}