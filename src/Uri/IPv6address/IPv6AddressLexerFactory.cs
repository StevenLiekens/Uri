using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.h16;
using UriSyntax.ls32;

namespace UriSyntax.IPv6address
{
    // ReSharper disable InconsistentNaming
    public class IPv6AddressLexerFactory : RuleLexerFactory<IPv6Address>
    {
        static IPv6AddressLexerFactory()
        {
            Default = new IPv6AddressLexerFactory(
                h16.HexadecimalInt16LexerFactory.Default.Singleton(),
                ls32.LeastSignificantInt32LexerFactory.Default.Singleton());
        }

        public IPv6AddressLexerFactory(
            [NotNull] ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory,
            [NotNull] ILexerFactory<LeastSignificantInt32> leastSignificantInt32LexerFactory)
        {
            if (hexadecimalInt16LexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16LexerFactory));
            }
            if (leastSignificantInt32LexerFactory == null)
            {
                throw new ArgumentNullException(nameof(leastSignificantInt32LexerFactory));
            }
            HexadecimalInt16LexerFactory = hexadecimalInt16LexerFactory;
            LeastSignificantInt32LexerFactory = leastSignificantInt32LexerFactory;
        }

        [NotNull]
        public static IPv6AddressLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HexadecimalInt16> HexadecimalInt16LexerFactory { get; }

        [NotNull]
        public ILexerFactory<LeastSignificantInt32> LeastSignificantInt32LexerFactory { get; }

        public override ILexer<IPv6Address> Create()
        {
            // ":"
            var colon = Terminal.Create(@":", StringComparer.Ordinal);

            // "::"
            var collapse = Terminal.Create(@"::", StringComparer.Ordinal);

            // h16 ":"
            var int16Lexer = HexadecimalInt16LexerFactory.Create();
            var h16c = Concatenation.Create(int16Lexer, colon);

            // h16-2
            var h16c2 =
                Alternation.Create(
                    Concatenation.Create(int16Lexer, colon, int16Lexer),
                    int16Lexer);

            // h16-3
            var h16c3 =
                Alternation.Create(
                    Concatenation.Create(Repetition.Create(h16c, 0, 2), int16Lexer),
                    h16c2);

            // h16-4
            var h16c4 =
                Alternation.Create(
                    Concatenation.Create(Repetition.Create(h16c, 0, 3), int16Lexer),
                    h16c3);

            // h16-5
            var h16c5 =
                Alternation.Create(
                    Concatenation.Create(Repetition.Create(h16c, 0, 4), int16Lexer),
                    h16c4);

            // h16-6
            var h16c6 =
                Alternation.Create(
                    Concatenation.Create(Repetition.Create(h16c, 0, 5), int16Lexer),
                    h16c5);

            // h16-7
            var h16c7 =
                Alternation.Create(
                    Concatenation.Create(Repetition.Create(h16c, 0, 6), int16Lexer),
                    h16c6);

            // 6( h16 ":" ) ls32
            var significantInt32Lexer = LeastSignificantInt32LexerFactory.Create();
            var alternation1 = Concatenation.Create(
                Repetition.Create(h16c, 6, 6),
                significantInt32Lexer);

            // "::" 5( h16 ":" ) ls32
            var alternation2 = Concatenation.Create(
                collapse,
                Repetition.Create(h16c, 5, 5),
                significantInt32Lexer);

            // [ h16 ] "::" 4( h16 ":" ) ls32
            var alternation3 = Concatenation.Create(
                Option.Create(int16Lexer),
                collapse,
                Repetition.Create(h16c, 4, 4),
                significantInt32Lexer);

            // [ h16-2 ] "::" 3( h16 ":" ) ls32
            var alternation4 = Concatenation.Create(
                Option.Create(h16c2),
                collapse,
                Repetition.Create(h16c, 3, 3),
                significantInt32Lexer);

            // [ h16-3 ] "::" 2( h16 ":" ) ls32
            var alternation5 = Concatenation.Create(
                Option.Create(h16c3),
                collapse,
                Repetition.Create(h16c, 2, 2),
                significantInt32Lexer);

            // [ h16-4 ] "::" h16 ":" ls32
            var alternation6 = Concatenation.Create(
                Option.Create(h16c4),
                collapse,
                int16Lexer,
                colon,
                significantInt32Lexer);

            // [ h16-5 ] "::" ls32
            var alternation7 = Concatenation.Create(
                Option.Create(h16c5),
                collapse,
                significantInt32Lexer);

            // [ h16-6 ] "::" h16
            var alternation8 = Concatenation.Create(
                Option.Create(h16c6),
                collapse,
                int16Lexer);

            // [ h16-7 ] "::"
            var alternation9 = Concatenation.Create(Option.Create(h16c7), collapse);
            var innerLexer = Alternation.Create(
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
