using Txt.ABNF;

namespace Uri.pchar
{
    public class PathCharacter : Alternation
    {
        public PathCharacter(Alternation element)
            : base(element)
        {
        }
    }
}