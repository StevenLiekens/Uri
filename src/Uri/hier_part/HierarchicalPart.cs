using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.hier_part
{
    public class HierarchicalPart : Alternation
    {
        public HierarchicalPart([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}