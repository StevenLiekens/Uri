using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.path_noscheme
{
    public class PathNoScheme : Concatenation
    {
        public PathNoScheme([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}