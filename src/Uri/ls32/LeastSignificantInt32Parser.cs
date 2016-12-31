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
            IPv4AddressParser = ipv4AddressParser;
            HexadecimalInt16Parser = hexadecimalInt16Parser;
        }

        [NotNull]
        public IParser<HexadecimalInt16, byte[]> HexadecimalInt16Parser { get; }

        // ReSharper disable once InconsistentNaming
        [NotNull]
        public IParser<IPv4Address, byte[]> IPv4AddressParser { get; }

        protected override byte[] ParseImpl(LeastSignificantInt32 value)
        {
            var ipv4Address = value.Element as IPv4Address;
            if (ipv4Address != null)
            {
                return IPv4AddressParser.Parse(ipv4Address);
            }
            var seq = (Concatenation)value.Element;
            return
                seq.OfType<HexadecimalInt16>()
                   .SelectMany(int16 => HexadecimalInt16Parser.Parse(int16))
                   .ToArray();
        }
    }
}
