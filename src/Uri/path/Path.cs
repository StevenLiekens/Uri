using Txt.ABNF;

namespace UriSyntax.path
{
    public class Path : Alternation
    {
        public Path(Alternation alternation)
            : base(alternation)
        {
        }
    }
}