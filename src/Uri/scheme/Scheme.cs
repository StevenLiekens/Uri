using Txt.ABNF;

namespace Uri.scheme
{
    public class Scheme : Concatenation
    {
        public Scheme(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}