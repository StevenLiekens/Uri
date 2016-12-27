using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.segment
{
    public class Segment : Repetition
    {
        public Segment([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}