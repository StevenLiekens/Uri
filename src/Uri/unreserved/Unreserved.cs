using Txt.ABNF;

namespace UriSyntax.unreserved
{
    public class Unreserved : Alternation
    {
        public Unreserved(Alternation element)
            : base(element)
        {
        }
    }
}