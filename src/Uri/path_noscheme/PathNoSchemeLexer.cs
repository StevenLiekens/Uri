using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path_noscheme
{
    public sealed class PathNoSchemeLexer : Lexer<PathNoScheme>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public PathNoSchemeLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathNoScheme> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<PathNoScheme>.FromResult(new PathNoScheme(result.Element));
            }
            return ReadResult<PathNoScheme>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}