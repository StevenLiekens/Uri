using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.query
{
    public class Query : Repetition
    {
        public Query([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}