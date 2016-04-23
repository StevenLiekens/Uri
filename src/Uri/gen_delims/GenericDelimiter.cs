using Txt.ABNF;

namespace Uri.gen_delims
{
    public class GenericDelimiter : Alternation
    {
        public GenericDelimiter(Alternation element)
            : base(element)
        {
        }
    }
}