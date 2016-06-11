﻿using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.relative_ref
{
    public sealed class RelativeReferenceLexer : Lexer<RelativeReference>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public RelativeReferenceLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<RelativeReference> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<RelativeReference>.FromResult(new RelativeReference(result.Element));
            }
            return
                ReadResult<RelativeReference>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
