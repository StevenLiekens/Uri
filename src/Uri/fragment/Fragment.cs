using Txt.ABNF;

namespace UriSyntax.fragment
{
    public class Fragment : Repetition
    {
        public Fragment(Repetition repetition)
            : base(repetition)
        {
        }
    }
}