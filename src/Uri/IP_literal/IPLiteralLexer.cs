using System;
using Txt.Core;
using Txt.ABNF;

namespace Uri.IP_literal
{
    public sealed class IPLiteralLexer : Lexer<IPLiteral>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public IPLiteralLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<IPLiteral> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<IPLiteral>.FromResult(new IPLiteral(result.Element));
            }
            return ReadResult<IPLiteral>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
