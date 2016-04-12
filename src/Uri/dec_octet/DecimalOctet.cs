using Txt.ABNF;

namespace Uri.dec_octet
{
    public class DecimalOctet : Alternative
    {
        public DecimalOctet(Alternative alternative)
            : base(alternative)
        {
        }

        public byte ToByte()
        {
            return byte.Parse(Text);
        }
    }
}