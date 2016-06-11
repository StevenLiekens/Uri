using Txt.ABNF;

namespace UriSyntax.reserved
{
    public class Reserved : Alternation
    {
        public Reserved(Alternation alternation)
            : base(alternation)
        {
        }
    }
}