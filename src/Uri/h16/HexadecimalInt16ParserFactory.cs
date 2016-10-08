using Txt.Core;

namespace UriSyntax.h16
{
    public class HexadecimalInt16ParserFactory : ParserFactory<HexadecimalInt16, byte[]>
    {
        public static IParserFactory<HexadecimalInt16, byte[]> Default { get; } = new HexadecimalInt16ParserFactory();

        public override IParser<HexadecimalInt16, byte[]> Create()
        {
            return new HexadecimalInt16Parser();
        }
    }
}
