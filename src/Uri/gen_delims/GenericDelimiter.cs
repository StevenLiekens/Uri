using Txt.ABNF;

namespace Uri.gen_delims
{
    public class GenericDelimiter : Alternative
    {
        public GenericDelimiter(Alternative element)
            : base(element)
        {
        }
    }
}