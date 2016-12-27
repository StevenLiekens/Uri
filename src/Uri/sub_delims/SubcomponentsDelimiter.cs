using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.sub_delims
{
    public class SubcomponentsDelimiter : Alternation
    {
        public SubcomponentsDelimiter([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}