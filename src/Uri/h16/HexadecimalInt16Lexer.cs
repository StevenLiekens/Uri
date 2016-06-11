using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.h16
{
    public sealed class HexadecimalInt16Lexer : Lexer<HexadecimalInt16>
    {
        private readonly ILexer<Repetition> innerLexer;

        public HexadecimalInt16Lexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<HexadecimalInt16> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<HexadecimalInt16>.FromResult(new HexadecimalInt16(result.Element));
            }
            return ReadResult<HexadecimalInt16>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}