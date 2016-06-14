using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.relative_ref
{
    public sealed class RelativeReferenceLexer : CompositeLexer<Concatenation, RelativeReference>
    {
        public RelativeReferenceLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
