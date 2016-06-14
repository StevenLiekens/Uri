using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.absolute_URI
{
    public sealed class AbsoluteUriLexer : CompositeLexer<Concatenation, AbsoluteUri>
    {
        public AbsoluteUriLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
