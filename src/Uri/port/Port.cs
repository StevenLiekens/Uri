using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.port
{
    public class Port : Repetition
    {
        public Port([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}