using Txt.ABNF;

namespace Uri.path
{
    public class Path : Alternative
    {
        public Path(Alternative alternative)
            : base(alternative)
        {
        }
    }
}