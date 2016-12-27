using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.h16;
using UriSyntax.ls32;

namespace UriSyntax.IPv6address
{
    // ReSharper disable InconsistentNaming
    public class IPv6AddressLexerFactory : LexerFactory<IPv6Address>
    {
        static IPv6AddressLexerFactory()
        {
            Default = new IPv6AddressLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                Txt.ABNF.OptionLexerFactory.Default,
                h16.HexadecimalInt16LexerFactory.Default.Singleton(),
                ls32.LeastSignificantInt32LexerFactory.Default.Singleton());
        }

        public IPv6AddressLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory,
            [NotNull] ILexerFactory<LeastSignificantInt32> leastSignificantInt32LexerFactory)
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
            if (hexadecimalInt16LexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16LexerFactory));
            }
            if (leastSignificantInt32LexerFactory == null)
            {
                throw new ArgumentNullException(nameof(leastSignificantInt32LexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            OptionLexerFactory = optionLexerFactory;
            HexadecimalInt16LexerFactory = hexadecimalInt16LexerFactory;
            LeastSignificantInt32LexerFactory = leastSignificantInt32LexerFactory;
        }

        [NotNull]
        public static IPv6AddressLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<HexadecimalInt16> HexadecimalInt16LexerFactory { get; }

        [NotNull]
        public ILexerFactory<LeastSignificantInt32> LeastSignificantInt32LexerFactory { get; }

        [NotNull]
        public IOptionLexerFactory OptionLexerFactory { get; }

        [NotNull]
        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<IPv6Address> Create()
        {
            // ":"
            var colon = TerminalLexerFactory.Create(@":", StringComparer.Ordinal);

            // "::"
            var collapse = TerminalLexerFactory.Create(@"::", StringComparer.Ordinal);

            // h16 ":"
            var int16Lexer = HexadecimalInt16LexerFactory.Create();
            var h16c = ConcatenationLexerFactory.Create(int16Lexer, colon);

            // h16-2
            var h16c2 =
                AlternationLexerFactory.Create(
                    ConcatenationLexerFactory.Create(int16Lexer, colon, int16Lexer),
                    int16Lexer);

            // h16-3
            var h16c3 =
                AlternationLexerFactory.Create(
                    ConcatenationLexerFactory.Create(RepetitionLexerFactory.Create(h16c, 0, 2), int16Lexer),
                    h16c2);

            // h16-4
            var h16c4 =
                AlternationLexerFactory.Create(
                    ConcatenationLexerFactory.Create(RepetitionLexerFactory.Create(h16c, 0, 3), int16Lexer),
                    h16c3);

            // h16-5
            var h16c5 =
                AlternationLexerFactory.Create(
                    ConcatenationLexerFactory.Create(RepetitionLexerFactory.Create(h16c, 0, 4), int16Lexer),
                    h16c4);

            // h16-6
            var h16c6 =
                AlternationLexerFactory.Create(
                    ConcatenationLexerFactory.Create(RepetitionLexerFactory.Create(h16c, 0, 5), int16Lexer),
                    h16c5);

            // h16-7
            var h16c7 =
                AlternationLexerFactory.Create(
                    ConcatenationLexerFactory.Create(RepetitionLexerFactory.Create(h16c, 0, 6), int16Lexer),
                    h16c6);

            // 6( h16 ":" ) ls32
            var significantInt32Lexer = LeastSignificantInt32LexerFactory.Create();
            var alternation1 = ConcatenationLexerFactory.Create(
                RepetitionLexerFactory.Create(h16c, 6, 6),
                significantInt32Lexer);

            // "::" 5( h16 ":" ) ls32
            var alternation2 = ConcatenationLexerFactory.Create(
                collapse,
                RepetitionLexerFactory.Create(h16c, 5, 5),
                significantInt32Lexer);

            // [ h16 ] "::" 4( h16 ":" ) ls32
            var alternation3 = ConcatenationLexerFactory.Create(
                OptionLexerFactory.Create(int16Lexer),
                collapse,
                RepetitionLexerFactory.Create(h16c, 4, 4),
                significantInt32Lexer);

            // [ h16-2 ] "::" 3( h16 ":" ) ls32
            var alternation4 = ConcatenationLexerFactory.Create(
                OptionLexerFactory.Create(h16c2),
                collapse,
                RepetitionLexerFactory.Create(h16c, 3, 3),
                significantInt32Lexer);

            // [ h16-3 ] "::" 2( h16 ":" ) ls32
            var alternation5 = ConcatenationLexerFactory.Create(
                OptionLexerFactory.Create(h16c3),
                collapse,
                RepetitionLexerFactory.Create(h16c, 2, 2),
                significantInt32Lexer);

            // [ h16-4 ] "::" h16 ":" ls32
            var alternation6 = ConcatenationLexerFactory.Create(
                OptionLexerFactory.Create(h16c4),
                collapse,
                int16Lexer,
                colon,
                significantInt32Lexer);

            // [ h16-5 ] "::" ls32
            var alternation7 = ConcatenationLexerFactory.Create(
                OptionLexerFactory.Create(h16c5),
                collapse,
                significantInt32Lexer);

            // [ h16-6 ] "::" h16
            var alternation8 = ConcatenationLexerFactory.Create(
                OptionLexerFactory.Create(h16c6),
                collapse,
                int16Lexer);

            // [ h16-7 ] "::"
            var alternation9 = ConcatenationLexerFactory.Create(OptionLexerFactory.Create(h16c7), collapse);
            var innerLexer = AlternationLexerFactory.Create(
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
