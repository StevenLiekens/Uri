using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.IPv4address
{
    // ReSharper disable once InconsistentNaming
    public class IPv4Address : Concatenation
    {
        public IPv4Address([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
