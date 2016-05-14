using System;
using JetBrains.Annotations;
using Txt.Core;
using Txt.ABNF;
using Uri.h16;
using Uri.IPv4address;

namespace Uri.ls32
{
    public class LeastSignificantInt32LexerFactory : ILexerFactory<LeastSignificantInt32>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HexadecimalInt16> hexadecimalInt16Lexer;

        private readonly ILexer<IPv4Address> ipv4AddressLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public LeastSignificantInt32LexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<HexadecimalInt16> hexadecimalInt16Lexer,
            [NotNull] ILexer<IPv4Address> ipv4AddressLexer)
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
            if (hexadecimalInt16Lexer == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16Lexer));
            }
            if (ipv4AddressLexer == null)
            {
                throw new ArgumentNullException(nameof(ipv4AddressLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.hexadecimalInt16Lexer = hexadecimalInt16Lexer;
            this.ipv4AddressLexer = ipv4AddressLexer;
        }

        public ILexer<LeastSignificantInt32> Create()
        {
            // ":"
            var b = terminalLexerFactory.Create(@":", StringComparer.Ordinal);

            // h16 ":" h16
            var c = concatenationLexerFactory.Create(hexadecimalInt16Lexer, b, hexadecimalInt16Lexer);

            // ( h16 ":" h16 ) / IPv4address
            var e = alternationLexerFactory.Create(c, ipv4AddressLexer);

            // ls32
            return new LeastSignificantInt32Lexer(e);
        }
    }
}
