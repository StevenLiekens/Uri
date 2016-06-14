using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.scheme
{
    public sealed class SchemeLexer : CompositeLexer<Concatenation, Scheme>
    {
        public SchemeLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
