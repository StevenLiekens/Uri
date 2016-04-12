using Txt.ABNF;

namespace Uri.query
{
    public class Query : Repetition
    {
        public Query(Repetition repetition)
            : base(repetition)
        {
        }
    }
}