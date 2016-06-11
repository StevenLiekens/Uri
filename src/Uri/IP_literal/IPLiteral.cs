using Txt.ABNF;

namespace UriSyntax.IP_literal
{
    public class IPLiteral : Concatenation
    {
        public IPLiteral(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}