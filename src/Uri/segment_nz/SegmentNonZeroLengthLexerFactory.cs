using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pchar;

namespace UriSyntax.segment_nz
{
    public class SegmentNonZeroLengthLexerFactory : ILexerFactory<SegmentNonZeroLength>
    {
        private readonly ILexer<PathCharacter> pathCharacterLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public SegmentNonZeroLengthLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<PathCharacter> pathCharacterLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (pathCharacterLexer == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.pathCharacterLexer = pathCharacterLexer;
        }

        public ILexer<SegmentNonZeroLength> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(pathCharacterLexer, 1, int.MaxValue);
            return new SegmentNonZeroLengthLexer(innerLexer);
        }
    }
}
