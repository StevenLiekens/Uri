using System;
using Txt;
using Txt.ABNF;

namespace Uri.relative_part
{
    public sealed class RelativePartLexer : Lexer<RelativePart>
    {
        private readonly ILexer<Alternation> innerLexer;

        public RelativePartLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<RelativePart> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<RelativePart>.FromResult(new RelativePart(result.Element));
            }
            return ReadResult<RelativePart>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}