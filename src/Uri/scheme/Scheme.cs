using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.scheme
{
    public class Scheme : Concatenation
    {
        public Scheme([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}