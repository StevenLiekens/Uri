using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.unreserved
{
    public class Unreserved : Alternation
    {
        public Unreserved([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}