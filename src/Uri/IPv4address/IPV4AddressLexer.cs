using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.IPv4address
{
    public sealed class IPv4AddressLexer : Lexer<IPv4Address>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public IPv4AddressLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<IPv4Address> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<IPv4Address>.FromResult(new IPv4Address(result.Element));
            }
            return ReadResult<IPv4Address>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
