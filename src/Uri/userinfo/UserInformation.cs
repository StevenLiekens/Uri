using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.userinfo
{
    public class UserInformation : Repetition
    {
        public UserInformation([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}