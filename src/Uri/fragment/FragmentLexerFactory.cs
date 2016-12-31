using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.pchar;

namespace UriSyntax.fragment
{
    public class FragmentLexerFactory : RuleLexerFactory<Fragment>
    {
        static FragmentLexerFactory()
        {
            Default = new FragmentLexerFactory(pchar.PathCharacterLexerFactory.Default.Singleton());
        }

        public FragmentLexerFactory([NotNull] ILexerFactory<PathCharacter> pathCharacterLexerFactory)
        {
            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexerFactory));
            }
            PathCharacterLexerFactory = pathCharacterLexerFactory;
        }

        [NotNull]
        public static FragmentLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<PathCharacter> PathCharacterLexerFactory { get; }

        public override ILexer<Fragment> Create()
        {
            var pathCharacterLexer = PathCharacterLexerFactory.Create();
            var alternationLexer = Alternation.Create(
                pathCharacterLexer,
                Terminal.Create(@"/", StringComparer.Ordinal),
                Terminal.Create(@"?", StringComparer.Ordinal));
            var fragmentRepetitionLexer = Repetition.Create(alternationLexer, 0, int.MaxValue);
            return new FragmentLexer(fragmentRepetitionLexer);
        }
    }
}
