using System;
using Txt.Core;
using Txt.ABNF;

namespace Uri.absolute_URI
{
    public sealed class AbsoluteUriLexer : Lexer<AbsoluteUri>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public AbsoluteUriLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<AbsoluteUri> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<AbsoluteUri>.FromResult(new AbsoluteUri(result.Element));
            }
            return ReadResult<AbsoluteUri>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
