using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.reg_name
{
    public sealed class RegisteredNameLexer : Lexer<RegisteredName>
    {
        private readonly ILexer<Repetition> innerLexer;

        public RegisteredNameLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<RegisteredName> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<RegisteredName>.FromResult(new RegisteredName(result.Element));
            }
            return ReadResult<RegisteredName>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}