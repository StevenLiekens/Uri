using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.reserved
{
    public class Reserved : Alternation
    {
        public Reserved([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}