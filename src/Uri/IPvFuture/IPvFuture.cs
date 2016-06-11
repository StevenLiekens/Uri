using Txt.ABNF;

namespace UriSyntax.IPvFuture
{
    public class IPvFuture : Concatenation
    {
        public IPvFuture(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}