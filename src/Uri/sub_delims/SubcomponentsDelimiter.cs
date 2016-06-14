using Txt.ABNF;

namespace UriSyntax.sub_delims
{
    public class SubcomponentsDelimiter : Alternation
    {
        public SubcomponentsDelimiter(Alternation element)
            : base(element)
        {
        }
    }
}
