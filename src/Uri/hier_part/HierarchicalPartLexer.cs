using System;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.hier_part
{
    public sealed class HierarchicalPartLexer : Lexer<HierarchicalPart>
    {
        private readonly ILexer<Alternation> innerLexer;

        public HierarchicalPartLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<HierarchicalPart> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<HierarchicalPart>.FromResult(new HierarchicalPart(result.Element));
            }
            return ReadResult<HierarchicalPart>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}