﻿using System;
using Txt.Core;
using Txt.ABNF;

namespace Uri.fragment
{
    public sealed class FragmentLexer : Lexer<Fragment>
    {
        private readonly ILexer<Repetition> innerLexer;

        public FragmentLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Fragment> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Fragment>.FromResult(new Fragment(result.Element));
            }
            return ReadResult<Fragment>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}