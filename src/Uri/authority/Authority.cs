using JetBrains.Annotations;
using Txt.ABNF;
using UriSyntax.host;
using UriSyntax.port;
using UriSyntax.userinfo;

namespace UriSyntax.authority
{
    public class Authority : Concatenation
    {
        public Authority([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }

        public Host Host => (Host)this[1];

        public Port Port
        {
            get
            {
                var optionalPort = (Repetition)this[2];
                if (optionalPort.Count == 0)
                {
                    return null;
                }
                var portConcatenation = (Concatenation)optionalPort[0];
                return (Port)portConcatenation[1];
            }
        }

        public UserInformation UserInformation
        {
            get
            {
                var optionalUserInfo = (Repetition)this[0];
                if (optionalUserInfo.Count == 0)
                {
                    return null;
                }
                var userInfoConcatenation = (Concatenation)optionalUserInfo[0];
                return (UserInformation)userInfoConcatenation[0];
            }
        }
    }
}
