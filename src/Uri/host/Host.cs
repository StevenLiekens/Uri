using Txt.ABNF;

namespace UriSyntax.host
{
    public class Host : Alternation
    {
        public Host(Alternation alternation)
            : base(alternation)
        {
        }
    }
}
