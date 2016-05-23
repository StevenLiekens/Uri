using System;
using System.Linq;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using Uri.h16;
using Uri.IPv4address;

namespace Uri.ls32
{
    public class LeastSignificantInt32Parser : Parser<LeastSignificantInt32, byte[]>
    {
        private readonly IParser<IPv4Address, byte[]> ipv4AddressParser;

        public LeastSignificantInt32Parser([NotNull] IParser<IPv4Address, byte[]> ipv4AddressParser)
        {
            if (ipv4AddressParser == null)
            {
                throw new ArgumentNullException(nameof(ipv4AddressParser));
            }
            this.ipv4AddressParser = ipv4AddressParser;
        }

        protected override byte[] ParseImpl(LeastSignificantInt32 value)
        {
            var ipv4Address = value.Element as IPv4Address;
            if (ipv4Address != null)
            {
                return ipv4AddressParser.Parse(ipv4Address);
            }

            var seq = (Concatenation)value.Element;
            return seq.Elements.OfType<HexadecimalInt16>().SelectMany(int16 => int16.GetBytes()).ToArray();
        }
    }
}
