using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.sub_delims
{
    public sealed class SubcomponentsDelimiterLexer : Lexer<SubcomponentsDelimiter>
    {
        private readonly ILexer<Alternation> innerLexer;

        public SubcomponentsDelimiterLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<SubcomponentsDelimiter> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<SubcomponentsDelimiter>.FromResult(new SubcomponentsDelimiter(result.Element));
            }
            return ReadResult<SubcomponentsDelimiter>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}