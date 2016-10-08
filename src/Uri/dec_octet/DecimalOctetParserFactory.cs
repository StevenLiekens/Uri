using Txt.Core;

namespace UriSyntax.dec_octet
{
    public class DecimalOctetParserFactory : ParserFactory<DecimalOctet, byte>
    {
        public static DecimalOctetParserFactory Default { get; } = new DecimalOctetParserFactory();

        public override IParser<DecimalOctet, byte> Create()
        {
            return new DecimalOctetParser();
        }
    }
}
