using System.Linq;
using Txt.ABNF;
using Uri.h16;
using Uri.IPv4address;

namespace Uri.ls32
{
    public class LeastSignificantInt32 : Alternation
    {
        public LeastSignificantInt32(Alternation alternation)
            : base(alternation)
        {
        }

        public byte[] GetBytes()
        {
            var thisAsIPv4 = Element as IPv4Address;
            if (thisAsIPv4 != null)
            {
                return thisAsIPv4.GetBytes();
            }

            var seq = (Concatenation)Element;
            return seq.Elements.OfType<HexadecimalInt16>().SelectMany(int16 => int16.GetBytes()).ToArray();
        }
    }
}