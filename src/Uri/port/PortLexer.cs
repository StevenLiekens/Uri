using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.port
{
    public sealed class PortLexer : CompositeLexer<Repetition, Port>
    {
        public PortLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
