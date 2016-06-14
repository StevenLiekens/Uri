using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.sub_delims
{
    public sealed class SubcomponentsDelimiterLexer : CompositeLexer<Alternation, SubcomponentsDelimiter>
    {
        public SubcomponentsDelimiterLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
