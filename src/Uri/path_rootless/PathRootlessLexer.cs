using System;
using Txt.Core;
using Txt.ABNF;

namespace Uri.path_rootless
{
    public sealed class PathRootlessLexer : Lexer<PathRootless>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public PathRootlessLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathRootless> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<PathRootless>.FromResult(new PathRootless(result.Element));
            }
            return ReadResult<PathRootless>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}