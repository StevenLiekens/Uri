using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pchar;

namespace UriSyntax.segment_nz
{
    public class SegmentNonZeroLengthLexerFactory : RuleLexerFactory<SegmentNonZeroLength>
    {
        static SegmentNonZeroLengthLexerFactory()
        {
            Default = new SegmentNonZeroLengthLexerFactory(
                pchar.PathCharacterLexerFactory.Default.Singleton());
        }

        public SegmentNonZeroLengthLexerFactory(
            [NotNull] ILexerFactory<PathCharacter> pathCharacterLexerFactory)
        {
            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexerFactory));
            }
            PathCharacterLexerFactory = pathCharacterLexerFactory;
        }

        [NotNull]
        public static SegmentNonZeroLengthLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<PathCharacter> PathCharacterLexerFactory { get; }

        public override ILexer<SegmentNonZeroLength> Create()
        {
            var pathCharacterLexer = PathCharacterLexerFactory.Create();
            var innerLexer = Repetition.Create(pathCharacterLexer, 1, int.MaxValue);
            return new SegmentNonZeroLengthLexer(innerLexer);
        }
    }
}
