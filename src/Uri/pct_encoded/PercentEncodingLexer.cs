using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.pct_encoded
{
    public sealed class PercentEncodingLexer : CompositeLexer<Concatenation, PercentEncoding>
    {
        public PercentEncodingLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
