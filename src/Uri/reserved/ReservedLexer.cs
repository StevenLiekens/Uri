using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.reserved
{
    public sealed class ReservedLexer : CompositeLexer<Alternation, Reserved>
    {
        public ReservedLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
