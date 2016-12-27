using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.path_empty
{
    public class PathEmpty : Repetition
    {
        public PathEmpty([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}