using Txt.ABNF;

namespace Uri.IP_literal
{
    public class IPLiteral : Concatenation
    {
        public IPLiteral(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}