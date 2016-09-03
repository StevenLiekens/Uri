using Txt.ABNF;

namespace UriSyntax.path_empty
{
    public class PathEmpty : Repetition
    {
        public PathEmpty(Repetition pathEmpty)
            : base(pathEmpty)
        {
        }
    }
}
