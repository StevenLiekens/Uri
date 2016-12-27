using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.ls32
{
    public class LeastSignificantInt32 : Alternation
    {
        public LeastSignificantInt32([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
