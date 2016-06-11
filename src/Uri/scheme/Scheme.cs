using Txt.ABNF;

namespace UriSyntax.scheme
{
    public class Scheme : Concatenation
    {
        public Scheme(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}