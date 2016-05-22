using Txt.ABNF;

namespace Uri.userinfo
{
    public class UserInformation : Repetition
    {
        public UserInformation(Repetition repetition)
            : base(repetition)
        {
        }
    }
}
