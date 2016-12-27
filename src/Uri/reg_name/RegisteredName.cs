using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.reg_name
{
    public class RegisteredName : Repetition
    {
        public RegisteredName([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}