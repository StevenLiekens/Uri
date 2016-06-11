using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.IPv6address
{
    public sealed class IPv6AddressLexer : Lexer<IPv6Address>
    {
        private readonly ILexer<Alternation> innerLexer;

        public IPv6AddressLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<IPv6Address> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<IPv6Address>.FromResult(new IPv6Address(result.Element));
            }
            return ReadResult<IPv6Address>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}