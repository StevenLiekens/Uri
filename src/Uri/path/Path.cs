using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.path
{
    public class Path : Alternation
    {
        public Path([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
