using Txt.ABNF;

namespace UriSyntax.reg_name
{
    public class RegisteredName : Repetition
    {
        public RegisteredName(Repetition repetition)
            : base(repetition)
        {
        }
    }
}
