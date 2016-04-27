using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.pchar;

namespace Uri.segment
{
    public class SegmentLexerFactory : ILexerFactory<Segment>
    {
        private readonly ILexer<PathCharacter> pathCharacterLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public SegmentLexerFactory(
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

        public ILexer<Segment> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(pathCharacterLexer, 0, int.MaxValue);
            return new SegmentLexer(innerLexer);
        }
    }
}
