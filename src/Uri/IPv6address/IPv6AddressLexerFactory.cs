// ReSharper disable InconsistentNaming

using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Uri.h16;
using Uri.ls32;

namespace Uri.IPv6address
{
    public class IPv6AddressLexerFactory : ILexerFactory<IPv6Address>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HexadecimalInt16> hexadecimalInt16Lexer;

        private readonly ILexer<LeastSignificantInt32> leastSignificantInt32Lexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public IPv6AddressLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<HexadecimalInt16> hexadecimalInt16Lexer,
            [NotNull] ILexer<LeastSignificantInt32> leastSignificantInt32Lexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (hexadecimalInt16Lexer == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16Lexer));
            }
            if (leastSignificantInt32Lexer == null)
            {
                throw new ArgumentNullException(nameof(leastSignificantInt32Lexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.hexadecimalInt16Lexer = hexadecimalInt16Lexer;
            this.leastSignificantInt32Lexer = leastSignificantInt32Lexer;
        }

        public ILexer<IPv6Address> Create()
        {
            // ":"
            var colon = terminalLexerFactory.Create(@":", StringComparer.Ordinal);

            // "::"
            var collapse = terminalLexerFactory.Create(@"::", StringComparer.Ordinal);

            // h16 ":"
            var h16c = concatenationLexerFactory.Create(hexadecimalInt16Lexer, colon);

            // h16-2
            var h16c2 =
                alternationLexerFactory.Create(
                    concatenationLexerFactory.Create(hexadecimalInt16Lexer, colon, hexadecimalInt16Lexer),
                    hexadecimalInt16Lexer);

            // h16-3
            var h16c3 =
                alternationLexerFactory.Create(
                    concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 0, 2), hexadecimalInt16Lexer),
                    h16c2);

            // h16-4
            var h16c4 =
                alternationLexerFactory.Create(
                    concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 0, 3), hexadecimalInt16Lexer),
                    h16c3);

            // h16-5
            var h16c5 =
                alternationLexerFactory.Create(
                    concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 0, 4), hexadecimalInt16Lexer),
                    h16c4);

            // h16-6
            var h16c6 =
                alternationLexerFactory.Create(
                    concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 0, 5), hexadecimalInt16Lexer),
                    h16c5);

            // h16-7
            var h16c7 =
                alternationLexerFactory.Create(
                    concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 0, 6), hexadecimalInt16Lexer),
                    h16c6);

            // 6( h16 ":" ) ls32
            var alternation1 = concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 6, 6), leastSignificantInt32Lexer);

            // "::" 5( h16 ":" ) ls32
            var alternation2 = concatenationLexerFactory.Create(
                collapse,
                repetitionLexerFactory.Create(h16c, 5, 5),
                leastSignificantInt32Lexer);

            // [ h16 ] "::" 4( h16 ":" ) ls32
            var alternation3 = concatenationLexerFactory.Create(
                optionLexerFactory.Create(hexadecimalInt16Lexer),
                collapse,
                repetitionLexerFactory.Create(h16c, 4, 4),
                leastSignificantInt32Lexer);

            // [ h16-2 ] "::" 3( h16 ":" ) ls32
            var alternation4 = concatenationLexerFactory.Create(
                optionLexerFactory.Create(h16c2),
                collapse,
                repetitionLexerFactory.Create(h16c, 3, 3),
                leastSignificantInt32Lexer);

            // [ h16-3 ] "::" 2( h16 ":" ) ls32
            var alternation5 = concatenationLexerFactory.Create(
                optionLexerFactory.Create(h16c3),
                collapse,
                repetitionLexerFactory.Create(h16c, 2, 2),
                leastSignificantInt32Lexer);

            // [ h16-4 ] "::" h16 ":" ls32
            var alternation6 = concatenationLexerFactory.Create(
                optionLexerFactory.Create(h16c4),
                collapse,
                hexadecimalInt16Lexer,
                colon,
                leastSignificantInt32Lexer);

            // [ h16-5 ] "::" ls32
            var alternation7 = concatenationLexerFactory.Create(optionLexerFactory.Create(h16c5), collapse, leastSignificantInt32Lexer);

            // [ h16-6 ] "::" h16
            var alternation8 = concatenationLexerFactory.Create(
                optionLexerFactory.Create(h16c6),
                collapse,
                hexadecimalInt16Lexer);

            // [ h16-7 ] "::"
            var alternation9 = concatenationLexerFactory.Create(optionLexerFactory.Create(h16c7), collapse);
            var innerLexer = alternationLexerFactory.Create(
                alternation1,
                alternation2,
                alternation3,
                alternation4,
                alternation5,
                alternation6,
                alternation7,
                alternation8,
                alternation9);
            return new IPv6AddressLexer(innerLexer);
        }
    }
}
