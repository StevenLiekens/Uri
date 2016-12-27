using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.IPv6address
{
    // ReSharper disable once InconsistentNaming
    public class IPv6Address : Alternation
    {
        public IPv6Address([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
