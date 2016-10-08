using System.Net;
using Txt.Core;

namespace UriSyntax.userinfo
{
    public class UserInformationParserFactory : ParserFactory<UserInformation, NetworkCredential>
    {
        public static UserInformationParserFactory Default { get; } = new UserInformationParserFactory();

        public override IParser<UserInformation, NetworkCredential> Create()
        {
            return new UserInformationParser();
        }
    }
}
