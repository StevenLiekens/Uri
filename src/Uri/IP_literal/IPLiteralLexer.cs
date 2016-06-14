using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.IP_literal
{
    public sealed class IPLiteralLexer : CompositeLexer<Concatenation, IPLiteral>
    {
        public IPLiteralLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
