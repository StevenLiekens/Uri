using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path
{
    public sealed class PathLexer : Lexer<Path>
    {
        private readonly ILexer<Alternation> innerLexer;

        public PathLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Path> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Path>.FromResult(new Path(result.Element));
            }
            return ReadResult<Path>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}