using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path_empty
{
    public sealed class PathEmptyLexer : Lexer<PathEmpty>
    {
        private readonly ILexer<Terminal> innerLexer;

        public PathEmptyLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathEmpty> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<PathEmpty>.FromResult(new PathEmpty(result.Element));
            }
            return ReadResult<PathEmpty>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
