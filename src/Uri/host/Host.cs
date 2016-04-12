using Txt.ABNF;

namespace Uri.host
{
    public class Host : Alternative
    {
        public Host(Alternative alternative)
            : base(alternative)
        {
        }
    }
}