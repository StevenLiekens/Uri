using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pchar;

namespace UriSyntax.segment_nz
{
    public class SegmentNonZeroLengthLexerFactory : LexerFactory<SegmentNonZeroLength>
    {
        static SegmentNonZeroLengthLexerFactory()
        {
            Default = new SegmentNonZeroLengthLexerFactory(
                Txt.ABNF.RepetitionLexerFactory.Default,
                pchar.PathCharacterLexerFactory.Default);
        }

        public SegmentNonZeroLengthLexerFactory(
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
            PathCharacterLexerFactory = pathCharacterLexerFactory.Singleton();
        }

        public static SegmentNonZeroLengthLexerFactory Default { get; }

        public ILexerFactory<PathCharacter> PathCharacterLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public override ILexer<SegmentNonZeroLength> Create()
        {
            var pathCharacterLexer = PathCharacterLexerFactory.Create();
            var innerLexer = RepetitionLexerFactory.Create(pathCharacterLexer, 1, int.MaxValue);
            return new SegmentNonZeroLengthLexer(innerLexer);
        }
    }
}
