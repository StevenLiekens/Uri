using System;
using System.Linq;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.h16;
using UriSyntax.IPv4address;

namespace UriSyntax.ls32
{
    public class LeastSignificantInt32Parser : Parser<LeastSignificantInt32, byte[]>
    {
        private readonly IParser<HexadecimalInt16, byte[]> hexadecimalInt16Parser;

        private readonly IParser<IPv4Address, byte[]> ipv4AddressParser;

        public LeastSignificantInt32Parser(
            [NotNull] IParser<IPv4Address, byte[]> ipv4AddressParser,
            [NotNull] IParser<HexadecimalInt16, byte[]> hexadecimalInt16Parser)
        {
            if (ipv4AddressParser == null)
            {
                throw new ArgumentNullException(nameof(ipv4AddressParser));
            }
            if (hexadecimalInt16Parser == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16Parser));
            }
            this.ipv4AddressParser = ipv4AddressParser;
            this.hexadecimalInt16Parser = hexadecimalInt16Parser;
        }

        protected override byte[] ParseImpl(LeastSignificantInt32 value)
        {
            var ipv4Address = value.Element as IPv4Address;
            if (ipv4Address != null)
            {
                return ipv4AddressParser.Parse(ipv4Address);
            }
            var seq = (Concatenation)value.Element;
            return
                seq.OfType<HexadecimalInt16>()
                   .SelectMany(int16 => hexadecimalInt16Parser.Parse(int16))
                   .ToArray();
        }
    }
}
