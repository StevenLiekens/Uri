using System;
using Txt.Core;
using Txt.ABNF;

namespace Uri.gen_delims
{
    public sealed class GenericDelimiterLexer : Lexer<GenericDelimiter>
    {
        private readonly ILexer<Alternation> innerLexer;

        public GenericDelimiterLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<GenericDelimiter> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<GenericDelimiter>.FromResult(new GenericDelimiter(result.Element));
            }
            return ReadResult<GenericDelimiter>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}