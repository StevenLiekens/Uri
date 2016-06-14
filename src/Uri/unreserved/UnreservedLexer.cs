using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.unreserved
{
    public sealed class UnreservedLexer : CompositeLexer<Alternation, Unreserved>
    {
        public UnreservedLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
