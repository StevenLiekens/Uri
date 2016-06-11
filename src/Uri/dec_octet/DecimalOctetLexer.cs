﻿using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.dec_octet
{
    public sealed class DecimalOctetLexer : Lexer<DecimalOctet>
    {
        private readonly ILexer<Alternation> innerLexer;

        public DecimalOctetLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<DecimalOctet> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<DecimalOctet>.FromResult(new DecimalOctet(result.Element));
            }
            return ReadResult<DecimalOctet>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
