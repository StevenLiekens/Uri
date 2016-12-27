using JetBrains.Annotations;
using Txt.Core;

namespace UriSyntax.dec_octet
{
    public class DecimalOctetParserFactory : ParserFactory<DecimalOctet, byte>
    {
        [NotNull]
        public static DecimalOctetParserFactory Default { get; } = new DecimalOctetParserFactory();

        public override IParser<DecimalOctet, byte> Create()
        {
            return new DecimalOctetParser();
        }
    }
}
