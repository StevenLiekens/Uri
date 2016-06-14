using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.fragment
{
    public sealed class FragmentLexer : CompositeLexer<Repetition, Fragment>
    {
        public FragmentLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
