using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.fragment
{
    public class Fragment : Repetition
    {
        public Fragment([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}