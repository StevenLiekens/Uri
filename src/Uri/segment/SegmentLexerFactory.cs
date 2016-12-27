using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pchar;

namespace UriSyntax.segment
{
    public class SegmentLexerFactory : LexerFactory<Segment>
    {
        static SegmentLexerFactory()
        {
            Default = new SegmentLexerFactory(
                Txt.ABNF.RepetitionLexerFactory.Default,
                pchar.PathCharacterLexerFactory.Default.Singleton());
        }

        public SegmentLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexerFactory<PathCharacter> pathCharacterLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexerFactory));
            }
            RepetitionLexerFactory = repetitionLexerFactory;
            PathCharacterLexerFactory = pathCharacterLexerFactory;
        }

        [NotNull]
        public static SegmentLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<PathCharacter> PathCharacterLexerFactory { get; }

        [NotNull]
        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public override ILexer<Segment> Create()
        {
            var pathCharacterLexer = PathCharacterLexerFactory.Create();
            var innerLexer = RepetitionLexerFactory.Create(pathCharacterLexer, 0, int.MaxValue);
            return new SegmentLexer(innerLexer);
        }
    }
}
