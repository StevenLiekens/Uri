using System.Net;
using Txt.Core;

namespace Uri.userinfo
{
    public class UserInformationParser : Parser<UserInformation, NetworkCredential>
    {
        protected override NetworkCredential ParseImpl(UserInformation value)
        {
            if (string.IsNullOrEmpty(value.Text))
            {
                return new NetworkCredential();
            }
            var indexOfSeparator = value.Text.IndexOf(':');
            if (indexOfSeparator == -1)
            {
                return new NetworkCredential { UserName = value.Text };
            }
            var userName = value.Text.Substring(0, indexOfSeparator);
            var password = value.Text.Substring(indexOfSeparator + 1);
            return new NetworkCredential(userName, password);
        }
    }
}
