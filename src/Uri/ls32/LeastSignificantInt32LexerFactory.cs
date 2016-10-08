using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.h16;
using UriSyntax.IPv4address;

namespace UriSyntax.ls32
{
    public class LeastSignificantInt32LexerFactory : LexerFactory<LeastSignificantInt32>
    {
        static LeastSignificantInt32LexerFactory()
        {
            Default = new LeastSignificantInt32LexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                h16.HexadecimalInt16LexerFactory.Default.Singleton(),
                IPv4AddressLexerFactory.Default.Singleton());
        }

        public LeastSignificantInt32LexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory,
            [NotNull] ILexerFactory<IPv4Address> ipv4AddressLexerFactory)
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
            if (hexadecimalInt16LexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16LexerFactory));
            }
            if (ipv4AddressLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipv4AddressLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            HexadecimalInt16LexerFactory = hexadecimalInt16LexerFactory;
            Ipv4AddressLexerFactory = ipv4AddressLexerFactory;
        }

        public static LeastSignificantInt32LexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<HexadecimalInt16> HexadecimalInt16LexerFactory { get; }

        public ILexerFactory<IPv4Address> Ipv4AddressLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<LeastSignificantInt32> Create()
        {
            // ":"
            var b = TerminalLexerFactory.Create(@":", StringComparer.Ordinal);

            // h16 ":" h16
            var int16Lexer = HexadecimalInt16LexerFactory.Create();
            var c = ConcatenationLexerFactory.Create(int16Lexer, b, int16Lexer);

            // ( h16 ":" h16 ) / IPv4address
            var ipv4Lexer = Ipv4AddressLexerFactory.Create();
            var e = AlternationLexerFactory.Create(c, ipv4Lexer);

            // ls32
            return new LeastSignificantInt32Lexer(e);
        }
    }
}
