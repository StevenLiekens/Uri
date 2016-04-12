using Txt.ABNF;

namespace Uri.reserved
{
    public class Reserved : Alternative
    {
        public Reserved(Alternative alternative)
            : base(alternative)
        {
        }
    }
}