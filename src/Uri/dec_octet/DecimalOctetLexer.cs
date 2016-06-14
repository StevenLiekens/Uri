using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.dec_octet
{
    public sealed class DecimalOctetLexer : CompositeLexer<Alternation, DecimalOctet>
    {
        public DecimalOctetLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
