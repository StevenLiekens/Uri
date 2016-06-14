using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.userinfo
{
    public sealed class UserInformationLexer : CompositeLexer<Repetition, UserInformation>
    {
        public UserInformationLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
