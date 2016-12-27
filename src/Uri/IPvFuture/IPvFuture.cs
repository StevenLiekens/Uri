using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.IPvFuture
{
    // ReSharper disable once InconsistentNaming
    public class IPvFuture : Concatenation
    {
        public IPvFuture([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
