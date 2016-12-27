using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.dec_octet
{
    public class DecimalOctet : Alternation
    {
        public DecimalOctet([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
