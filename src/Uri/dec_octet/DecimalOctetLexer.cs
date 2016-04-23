using System;
using Txt;
using Txt.ABNF;

namespace Uri.dec_octet
{
    public sealed class DecimalOctetLexer : Lexer<DecimalOctet>
    {
        private readonly ILexer<Alternation> innerLexer;

        public DecimalOctetLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<DecimalOctet> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<DecimalOctet>.FromResult(new DecimalOctet(result.Element));
            }
            return ReadResult<DecimalOctet>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
