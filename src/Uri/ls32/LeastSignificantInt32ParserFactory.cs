using System;
using JetBrains.Annotations;
using Txt.Core;
using UriSyntax.h16;
using UriSyntax.IPv4address;

namespace UriSyntax.ls32
{
    public class LeastSignificantInt32ParserFactory : ParserFactory<LeastSignificantInt32, byte[]>
    {
        static LeastSignificantInt32ParserFactory()
        {
            Default = new LeastSignificantInt32ParserFactory(
                IPv4address.IPv4AddressParserFactory.Default.Singleton(),
                h16.HexadecimalInt16ParserFactory.Default.Singleton());
        }

        public LeastSignificantInt32ParserFactory(
            [NotNull] IParserFactory<IPv4Address, byte[]> ipv4AddressParserFactory,
            [NotNull] IParserFactory<HexadecimalInt16, byte[]> hexadecimalInt16ParserFactory)
        {
            if (ipv4AddressParserFactory == null)
            {
                throw new ArgumentNullException(nameof(ipv4AddressParserFactory));
            }
            if (hexadecimalInt16ParserFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16ParserFactory));
            }
            IPv4AddressParserFactory = ipv4AddressParserFactory;
            HexadecimalInt16ParserFactory = hexadecimalInt16ParserFactory;
        }

        public static IParserFactory<LeastSignificantInt32, byte[]> Default { get; }

        public IParserFactory<HexadecimalInt16, byte[]> HexadecimalInt16ParserFactory { get; }

        public IParserFactory<IPv4Address, byte[]> IPv4AddressParserFactory { get; }

        public override IParser<LeastSignificantInt32, byte[]> Create()
        {
            return new LeastSignificantInt32Parser(
                IPv4AddressParserFactory.Create(),
                HexadecimalInt16ParserFactory.Create());
        }
    }
}
