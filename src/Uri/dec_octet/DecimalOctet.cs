using Txt.ABNF;

namespace Uri.dec_octet
{
    public class DecimalOctet : Alternation
    {
        public DecimalOctet(Alternation alternation)
            : base(alternation)
        {
        }

        public byte ToByte()
        {
            return byte.Parse(Text);
        }
    }
}