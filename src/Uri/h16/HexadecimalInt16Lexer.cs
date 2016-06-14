using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.h16
{
    public sealed class HexadecimalInt16Lexer : CompositeLexer<Repetition, HexadecimalInt16>
    {
        public HexadecimalInt16Lexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
