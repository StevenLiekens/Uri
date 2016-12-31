using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pchar;

namespace UriSyntax.segment
{
    public class SegmentLexerFactory : RuleLexerFactory<Segment>
    {
        static SegmentLexerFactory()
        {
            Default = new SegmentLexerFactory(
                pchar.PathCharacterLexerFactory.Default.Singleton());
        }

        public SegmentLexerFactory(
            [NotNull] ILexerFactory<PathCharacter> pathCharacterLexerFactory)
        {
            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexerFactory));
            }
            PathCharacterLexerFactory = pathCharacterLexerFactory;
        }

        [NotNull]
        public static SegmentLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<PathCharacter> PathCharacterLexerFactory { get; }

        public override ILexer<Segment> Create()
        {
            var pathCharacterLexer = PathCharacterLexerFactory.Create();
            var innerLexer = Repetition.Create(pathCharacterLexer, 0, int.MaxValue);
            return new SegmentLexer(innerLexer);
        }
    }
}
