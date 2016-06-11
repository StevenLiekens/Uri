using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.relative_part
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

        public override ReadResult<RelativePart> ReadImpl(ITextScanner scanner)
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