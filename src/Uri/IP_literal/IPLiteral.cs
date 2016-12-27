using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.IP_literal
{
    public class IPLiteral : Concatenation
    {
        public IPLiteral([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
