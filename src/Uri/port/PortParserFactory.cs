using Txt.Core;

namespace UriSyntax.port
{
    public class PortParserFactory : ParserFactory<Port, int>
    {
        public static PortParserFactory Default { get; } = new PortParserFactory();

        public override IParser<Port, int> Create()
        {
            return new PortParser();
        }
    }
}
