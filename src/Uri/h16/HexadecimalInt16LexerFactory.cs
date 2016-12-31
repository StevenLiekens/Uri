using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;
using Txt.Core;

namespace UriSyntax.h16
{
    public class HexadecimalInt16LexerFactory : RuleLexerFactory<HexadecimalInt16>
    {
        static HexadecimalInt16LexerFactory()
        {
            Default = new HexadecimalInt16LexerFactory(HexadecimalDigitLexerFactory.Default.Singleton());
        }

        public HexadecimalInt16LexerFactory(
            [NotNull] ILexerFactory<HexadecimalDigit> hexadecimalLexerFactory)
        {
            if (hexadecimalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalLexerFactory));
            }
            HexadecimalLexerFactory = hexadecimalLexerFactory;
        }

        [NotNull]
        public static HexadecimalInt16LexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HexadecimalDigit> HexadecimalLexerFactory { get; }

        public override ILexer<HexadecimalInt16> Create()
        {
            var innerLexer = Repetition.Create(HexadecimalLexerFactory.Create(), 1, 4);
            return new HexadecimalInt16Lexer(innerLexer);
        }
    }
}
