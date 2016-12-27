using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.gen_delims
{
    public class GenericDelimiter : Alternation
    {
        public GenericDelimiter([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}