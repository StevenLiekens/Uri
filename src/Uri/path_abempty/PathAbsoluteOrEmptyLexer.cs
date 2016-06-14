using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path_abempty
{
    public sealed class PathAbsoluteOrEmptyLexer : CompositeLexer<Repetition, PathAbsoluteOrEmpty>
    {
        public PathAbsoluteOrEmptyLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
