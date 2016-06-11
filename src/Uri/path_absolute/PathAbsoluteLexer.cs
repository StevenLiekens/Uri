using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.path_absolute
{
    public sealed class PathAbsoluteLexer : Lexer<PathAbsolute>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public PathAbsoluteLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathAbsolute> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<PathAbsolute>.FromResult(new PathAbsolute(result.Element));
            }
            return ReadResult<PathAbsolute>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}