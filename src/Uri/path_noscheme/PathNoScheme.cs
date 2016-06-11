using Txt.ABNF;

namespace UriSyntax.path_noscheme
{
    public class PathNoScheme : Concatenation
    {
        public PathNoScheme(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}