using System;
using Txt.Core;
using Txt.ABNF;

namespace Uri.IPvFuture
{
    public sealed class IPvFutureLexer : Lexer<IPvFuture>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public IPvFutureLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<IPvFuture> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<IPvFuture>.FromResult(new IPvFuture(result.Element));
            }
            return ReadResult<IPvFuture>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}