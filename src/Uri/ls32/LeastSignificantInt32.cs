using Txt.ABNF;

namespace Uri.ls32
{
    public class LeastSignificantInt32 : Alternation
    {
        public LeastSignificantInt32(Alternation alternation)
            : base(alternation)
        {
        }
    }
}
