using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.reg_name
{
    public sealed class RegisteredNameLexer : CompositeLexer<Repetition, RegisteredName>
    {
        public RegisteredNameLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
