using System;
using JetBrains.Annotations;
using Txt.Core;
using UriSyntax.h16;
using UriSyntax.ls32;

namespace UriSyntax.IPv6address
{
    public class IPv6AddressParserFactory : ParserFactory<IPv6Address, byte[]>
    {
        private readonly IParserFactory<HexadecimalInt16, byte[]> hexadecimalInt16ParserFactory;

        private readonly IParserFactory<LeastSignificantInt32, byte[]> leastSignificantInt32ParserFactory;

        static IPv6AddressParserFactory()
        {
            Default = new IPv6AddressParserFactory(
                HexadecimalInt16ParserFactory.Default.Singleton(),
                LeastSignificantInt32ParserFactory.Default.Singleton());
        }

        public IPv6AddressParserFactory(
            [NotNull] IParserFactory<HexadecimalInt16, byte[]> hexadecimalInt16ParserFactory,
            [NotNull] IParserFactory<LeastSignificantInt32, byte[]> leastSignificantInt32ParserFactory)
        {
            if (hexadecimalInt16ParserFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16ParserFactory));
            }
            if (leastSignificantInt32ParserFactory == null)
            {
                throw new ArgumentNullException(nameof(leastSignificantInt32ParserFactory));
            }
            this.hexadecimalInt16ParserFactory = hexadecimalInt16ParserFactory;
            this.leastSignificantInt32ParserFactory = leastSignificantInt32ParserFactory;
        }

        public static IPv6AddressParserFactory Default { get; }

        public override IParser<IPv6Address, byte[]> Create()
        {
            return new IPv6AddressParser(
                hexadecimalInt16ParserFactory.Create(),
                leastSignificantInt32ParserFactory.Create());
        }
    }
}
