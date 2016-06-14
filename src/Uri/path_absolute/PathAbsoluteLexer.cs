using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path_absolute
{
    public sealed class PathAbsoluteLexer : CompositeLexer<Concatenation, PathAbsolute>
    {
        public PathAbsoluteLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
