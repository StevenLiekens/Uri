using Txt.ABNF;

namespace UriSyntax.query
{
    public class Query : Repetition
    {
        public Query(Repetition repetition)
            : base(repetition)
        {
        }
    }
}
