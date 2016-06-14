using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.URI_reference
{
    public sealed class UriReferenceLexer : CompositeLexer<Alternation, UriReference>
    {
        public UriReferenceLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
