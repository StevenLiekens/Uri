using JetBrains.Annotations;
using Txt.Core;

namespace UriSyntax.h16
{
    public class HexadecimalInt16ParserFactory : ParserFactory<HexadecimalInt16, byte[]>
    {
        static HexadecimalInt16ParserFactory()
        {
            Default = new HexadecimalInt16ParserFactory();
        }

        [NotNull]
        public static IParserFactory<HexadecimalInt16, byte[]> Default { get; }

        public override IParser<HexadecimalInt16, byte[]> Create()
        {
            return new HexadecimalInt16Parser();
        }
    }
}
