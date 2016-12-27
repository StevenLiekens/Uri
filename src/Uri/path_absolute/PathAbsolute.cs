using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.path_absolute
{
    public class PathAbsolute : Concatenation
    {
        public PathAbsolute([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}