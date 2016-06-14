using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.IPvFuture
{
    public sealed class IPvFutureLexer : CompositeLexer<Concatenation, IPvFuture>
    {
        public IPvFutureLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
