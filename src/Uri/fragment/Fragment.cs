using Txt.ABNF;

namespace Uri.fragment
{
    public class Fragment : Repetition
    {
        public Fragment(Repetition repetition)
            : base(repetition)
        {
        }
    }
}