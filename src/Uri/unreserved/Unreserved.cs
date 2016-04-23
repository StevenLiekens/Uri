using Txt.ABNF;

namespace Uri.unreserved
{
    public class Unreserved : Alternation
    {
        public Unreserved(Alternation element)
            : base(element)
        {
        }
    }
}