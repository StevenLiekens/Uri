using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.query
{
    public sealed class QueryLexer : CompositeLexer<Repetition, Query>
    {
        public QueryLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
