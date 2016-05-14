using System;
using Txt.Core;
using Txt.ABNF;

namespace Uri.segment
{
    public sealed class SegmentLexer : Lexer<Segment>
    {
        private readonly ILexer<Repetition> innerLexer;

        public SegmentLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Segment> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Segment>.FromResult(new Segment(result.Element));
            }
            return ReadResult<Segment>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
