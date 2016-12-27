using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.path_abempty
{
    public class PathAbsoluteOrEmpty : Repetition
    {
        public PathAbsoluteOrEmpty([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}