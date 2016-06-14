using Txt.ABNF;

namespace UriSyntax.pchar
{
    public class PathCharacter : Alternation
    {
        public PathCharacter(Alternation element)
            : base(element)
        {
        }
    }
}
