using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.ls32
{
    public sealed class LeastSignificantInt32Lexer : CompositeLexer<Alternation, LeastSignificantInt32>
    {
        public LeastSignificantInt32Lexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
