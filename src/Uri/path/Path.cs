using Txt.ABNF;

namespace Uri.path
{
    public class Path : Alternation
    {
        public Path(Alternation alternation)
            : base(alternation)
        {
        }
    }
}