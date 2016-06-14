using Txt.ABNF;

namespace UriSyntax.segment
{
    public class Segment : Repetition
    {
        public Segment(Repetition repetition)
            : base(repetition)
        {
        }
    }
}
