using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.pct_encoded
{
    public class PercentEncoding : Concatenation
    {
        public PercentEncoding([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}