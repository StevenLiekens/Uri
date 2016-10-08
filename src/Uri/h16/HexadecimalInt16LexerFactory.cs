using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;
using Txt.Core;

namespace UriSyntax.h16
{
    public class HexadecimalInt16LexerFactory : LexerFactory<HexadecimalInt16>
    {
        static HexadecimalInt16LexerFactory()
        {
            Default = new HexadecimalInt16LexerFactory(
                Txt.ABNF.RepetitionLexerFactory.Default,
                HexadecimalDigitLexerFactory.Default.Singleton());
        }

        public HexadecimalInt16LexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexerFactory<HexadecimalDigit> hexadecimalLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (hexadecimalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalLexerFactory));
            }
            RepetitionLexerFactory = repetitionLexerFactory;
            HexadecimalLexerFactory = hexadecimalLexerFactory;
        }

        public static HexadecimalInt16LexerFactory Default { get; }

        public ILexerFactory<HexadecimalDigit> HexadecimalLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public override ILexer<HexadecimalInt16> Create()
        {
            var innerLexer = RepetitionLexerFactory.Create(HexadecimalLexerFactory.Create(), 1, 4);
            return new HexadecimalInt16Lexer(innerLexer);
        }
    }
}
