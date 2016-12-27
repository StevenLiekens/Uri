using JetBrains.Annotations;
using Txt.ABNF;

namespace UriSyntax.pchar
{
    public class PathCharacter : Alternation
    {
        public PathCharacter([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}