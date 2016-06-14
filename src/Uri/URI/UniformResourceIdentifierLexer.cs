using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.URI
{
    public sealed class UniformResourceIdentifierLexer : CompositeLexer<Concatenation, UniformResourceIdentifier>
    {
        public UniformResourceIdentifierLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
