using System;
using System.Linq;
using JetBrains.Annotations;
using Txt.Core;
using UriSyntax.dec_octet;

namespace UriSyntax.IPv4address
{
    public class IPv4AddressParser : Parser<IPv4Address, byte[]>
    {
        private readonly IParser<DecimalOctet, byte> decimalOctetParser;

        public IPv4AddressParser([NotNull] IParser<DecimalOctet, byte> decimalOctetParser)
        {
            if (decimalOctetParser == null)
            {
                throw new ArgumentNullException(nameof(decimalOctetParser));
            }
            this.decimalOctetParser = decimalOctetParser;
        }

        protected override byte[] ParseImpl(IPv4Address value)
        {
            return value.OfType<DecimalOctet>().Select(decimalOctetParser.Parse).ToArray();
        }
    }
}
