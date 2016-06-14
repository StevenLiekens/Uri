using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.relative_part
{
    public sealed class RelativePartLexer : CompositeLexer<Alternation, RelativePart>
    {
        public RelativePartLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
