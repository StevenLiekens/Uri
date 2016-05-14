using System;
using Txt.Core;
using Txt.ABNF;

namespace Uri.userinfo
{
    public sealed class UserInformationLexer : Lexer<UserInformation>
    {
        private readonly ILexer<Repetition> innerLexer;

        public UserInformationLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<UserInformation> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<UserInformation>.FromResult(new UserInformation(result.Element));
            }
            return ReadResult<UserInformation>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}