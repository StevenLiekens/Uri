using Txt.ABNF;

namespace Uri.reserved
{
    public class Reserved : Alternation
    {
        public Reserved(Alternation alternation)
            : base(alternation)
        {
        }
    }
}