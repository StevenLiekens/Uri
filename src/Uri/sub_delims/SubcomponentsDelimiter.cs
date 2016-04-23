using Txt.ABNF;

namespace Uri.sub_delims
{
    public class SubcomponentsDelimiter : Alternation
    {
        public SubcomponentsDelimiter(Alternation element)
            : base(element)
        {
        }
    }
}