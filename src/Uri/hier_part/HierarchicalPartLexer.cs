using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.hier_part
{
    public sealed class HierarchicalPartLexer : CompositeLexer<Alternation, HierarchicalPart>
    {
        public HierarchicalPartLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
