using System;
using Txt.Core;
using Txt.ABNF;

namespace Uri.reserved
{
    public sealed class ReservedLexer : Lexer<Reserved>
    {
        private readonly ILexer<Alternation> innerLexer;

        public ReservedLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Reserved> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Reserved>.FromResult(new Reserved(result.Element));
            }
            return ReadResult<Reserved>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}