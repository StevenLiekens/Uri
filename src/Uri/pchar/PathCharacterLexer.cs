﻿using System;
using Txt;
using Txt.ABNF;

namespace Uri.pchar
{
    public sealed class PathCharacterLexer : Lexer<PathCharacter>
    {
        private readonly ILexer<Alternation> innerLexer;

        public PathCharacterLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathCharacter> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<PathCharacter>.FromResult(new PathCharacter(result.Element));
            }
            return ReadResult<PathCharacter>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}