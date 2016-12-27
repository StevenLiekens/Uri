using System;
using JetBrains.Annotations;
using Txt.Core;
using UriSyntax.dec_octet;

namespace UriSyntax.IPv4address
{
    // ReSharper disable once InconsistentNaming
    public class IPv4AddressParserFactory : ParserFactory<IPv4Address, byte[]>
    {
        static IPv4AddressParserFactory()
        {
            Default = new IPv4AddressParserFactory(dec_octet.DecimalOctetParserFactory.Default.Singleton());
        }

        public IPv4AddressParserFactory([NotNull] IParserFactory<DecimalOctet, byte> decimalOctetParserFactory)
        {
            if (decimalOctetParserFactory == null)
            {
                throw new ArgumentNullException(nameof(decimalOctetParserFactory));
            }
            DecimalOctetParserFactory = decimalOctetParserFactory;
        }

        [NotNull]
        public static IParserFactory<IPv4Address, byte[]> Default { get; }

        [NotNull]
        public IParserFactory<DecimalOctet, byte> DecimalOctetParserFactory { get; }

        public override IParser<IPv4Address, byte[]> Create()
        {
            return new IPv4AddressParser(DecimalOctetParserFactory.Create());
        }
    }
}
