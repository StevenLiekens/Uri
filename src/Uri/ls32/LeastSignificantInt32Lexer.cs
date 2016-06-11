using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.ls32
{
    public sealed class LeastSignificantInt32Lexer : Lexer<LeastSignificantInt32>
    {
        private readonly ILexer<Alternation> innerLexer;

        public LeastSignificantInt32Lexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<LeastSignificantInt32> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<LeastSignificantInt32>.FromResult(new LeastSignificantInt32(result.Element));
            }
            return
                ReadResult<LeastSignificantInt32>.FromSyntaxError(
                    SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
