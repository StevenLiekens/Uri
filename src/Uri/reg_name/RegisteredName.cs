using Txt.ABNF;

namespace Uri.reg_name
{
    public class RegisteredName : Repetition
    {
        public RegisteredName(Repetition repetition)
            : base(repetition)
        {
        }
    }
}