using System;
using System.Linq;
using JetBrains.Annotations;
using Txt.Core;
using UriSyntax.dec_octet;

namespace UriSyntax.IPv4address
{
    // ReSharper disable once InconsistentNaming
    public class IPv4AddressParser : Parser<IPv4Address, byte[]>
    {
        public IPv4AddressParser([NotNull] IParser<DecimalOctet, byte> decimalOctetParser)
        {
            if (decimalOctetParser == null)
            {
                throw new ArgumentNullException(nameof(decimalOctetParser));
            }
            DecimalOctetParser = decimalOctetParser;
        }

        [NotNull]
        public IParser<DecimalOctet, byte> DecimalOctetParser { get; }

        protected override byte[] ParseImpl(IPv4Address value)
        {
            return value.OfType<DecimalOctet>().Select(DecimalOctetParser.Parse).ToArray();
        }
    }
}
