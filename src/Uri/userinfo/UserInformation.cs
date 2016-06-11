using Txt.ABNF;

namespace UriSyntax.userinfo
{
    public class UserInformation : Repetition
    {
        public UserInformation(Repetition repetition)
            : base(repetition)
        {
        }
    }
}
