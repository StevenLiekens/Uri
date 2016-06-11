﻿using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.pct_encoded
{
    public sealed class PercentEncodingLexer : Lexer<PercentEncoding>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public PercentEncodingLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PercentEncoding> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<PercentEncoding>.FromResult(new PercentEncoding(result.Element));
            }
            return ReadResult<PercentEncoding>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}