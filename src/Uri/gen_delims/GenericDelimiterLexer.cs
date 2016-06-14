using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.gen_delims
{
    public sealed class GenericDelimiterLexer : CompositeLexer<Alternation, GenericDelimiter>
    {
        public GenericDelimiterLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
