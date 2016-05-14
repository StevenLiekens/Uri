using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;

namespace Uri.h16
{
    public class HexadecimalInt16LexerFactory : ILexerFactory<HexadecimalInt16>
    {
        private readonly ILexer<HexadecimalDigit> hexadecimalLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public HexadecimalInt16LexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<HexadecimalDigit> hexadecimalLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (hexadecimalLexer == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.hexadecimalLexer = hexadecimalLexer;
        }

        public ILexer<HexadecimalInt16> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(hexadecimalLexer, 1, 4);
            return new HexadecimalInt16Lexer(innerLexer);
        }
    }
}
