using Txt.ABNF;

namespace Uri.host
{
    public class Host : Alternation
    {
        public Host(Alternation alternation)
            : base(alternation)
        {
        }
    }
}