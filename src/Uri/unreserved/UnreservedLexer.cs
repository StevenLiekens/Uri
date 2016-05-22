﻿using System;
using Txt.Core;
using Txt.ABNF;

namespace Uri.unreserved
{
    public sealed class UnreservedLexer : Lexer<Unreserved>
    {
        private readonly ILexer<Alternation> innerLexer;

        public UnreservedLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Unreserved> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Unreserved>.FromResult(new Unreserved(result.Element));
            }
            return ReadResult<Unreserved>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}