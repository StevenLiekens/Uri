using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.host
{
    public class Host : Alternation
    {
        public Host([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
