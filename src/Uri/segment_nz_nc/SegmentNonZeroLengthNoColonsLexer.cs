using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.segment_nz_nc
{
    public class SegmentNonZeroLengthNoColonsLexer : Lexer<SegmentNonZeroLengthNoColons>
    {
        public SegmentNonZeroLengthNoColonsLexer([NotNull] ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Repetition> InnerLexer { get; }

        protected override IEnumerable<SegmentNonZeroLengthNoColons> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var repetition in InnerLexer.Read(scanner, context))
            {
                yield return new SegmentNonZeroLengthNoColons(repetition);
            }
        }
    }
}