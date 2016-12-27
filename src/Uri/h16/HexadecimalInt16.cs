using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.h16
{
    public class HexadecimalInt16 : Repetition
    {
        public HexadecimalInt16([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}