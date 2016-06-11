using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.scheme
{
    public sealed class SchemeLexer : Lexer<Scheme>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public SchemeLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Scheme> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Scheme>.FromResult(new Scheme(result.Element));
            }
            return ReadResult<Scheme>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}