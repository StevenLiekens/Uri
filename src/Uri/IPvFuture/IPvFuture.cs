using Txt.ABNF;

namespace Uri.IPvFuture
{
    public class IPvFuture : Concatenation
    {
        public IPvFuture(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}