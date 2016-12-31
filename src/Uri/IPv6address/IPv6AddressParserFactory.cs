using System;
using JetBrains.Annotations;
using Txt.Core;
using UriSyntax.h16;
using UriSyntax.ls32;

namespace UriSyntax.IPv6address
{
    // ReSharper disable once InconsistentNaming
    public class IPv6AddressParserFactory : ParserFactory<IPv6Address, byte[]>
    {
        static IPv6AddressParserFactory()
        {
            Default = new IPv6AddressParserFactory(
                h16.HexadecimalInt16ParserFactory.Default.Singleton(),
                ls32.LeastSignificantInt32ParserFactory.Default.Singleton());
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
            HexadecimalInt16ParserFactory = hexadecimalInt16ParserFactory;
            LeastSignificantInt32ParserFactory = leastSignificantInt32ParserFactory;
        }

        [NotNull]
        public static IPv6AddressParserFactory Default { get; }

        [NotNull]
        public IParserFactory<HexadecimalInt16, byte[]> HexadecimalInt16ParserFactory { get; }

        [NotNull]
        public IParserFactory<LeastSignificantInt32, byte[]> LeastSignificantInt32ParserFactory { get; }

        public override IParser<IPv6Address, byte[]> Create()
        {
            return new IPv6AddressParser(
                HexadecimalInt16ParserFactory.Create(),
                LeastSignificantInt32ParserFactory.Create());
        }
    }
}
