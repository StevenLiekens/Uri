using Txt.Core;

namespace UriSyntax.pct_encoded
{
    public class PercentEncodingParserFactory : ParserFactory<PercentEncoding, char>
    {
        static PercentEncodingParserFactory()
        {
            Default = new PercentEncodingParserFactory();
        }

        public static PercentEncodingParserFactory Default { get; }

        public override IParser<PercentEncoding, char> Create()
        {
            return new PercentEncodingParser();
        }
    }
}
