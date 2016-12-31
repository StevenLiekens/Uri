using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.h16;
using UriSyntax.IPv4address;

namespace UriSyntax.ls32
{
    public class LeastSignificantInt32LexerFactory : RuleLexerFactory<LeastSignificantInt32>
    {
        static LeastSignificantInt32LexerFactory()
        {
            Default = new LeastSignificantInt32LexerFactory(
                h16.HexadecimalInt16LexerFactory.Default.Singleton(),
                IPv4AddressLexerFactory.Default.Singleton());
        }

        public LeastSignificantInt32LexerFactory(
            [NotNull] ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory,
            [NotNull] ILexerFactory<IPv4Address> ipv4AddressLexerFactory)
        {
            if (hexadecimalInt16LexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16LexerFactory));
            }
            if (ipv4AddressLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipv4AddressLexerFactory));
            }
            HexadecimalInt16LexerFactory = hexadecimalInt16LexerFactory;
            Ipv4AddressLexerFactory = ipv4AddressLexerFactory;
        }

        [NotNull]
        public static LeastSignificantInt32LexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HexadecimalInt16> HexadecimalInt16LexerFactory { get; }

        [NotNull]
        public ILexerFactory<IPv4Address> Ipv4AddressLexerFactory { get; }

        public override ILexer<LeastSignificantInt32> Create()
        {
            // ":"
            var b = Terminal.Create(@":", StringComparer.Ordinal);

            // h16 ":" h16
            var int16Lexer = HexadecimalInt16LexerFactory.Create();
            var c = Concatenation.Create(int16Lexer, b, int16Lexer);

            // ( h16 ":" h16 ) / IPv4address
            var ipv4Lexer = Ipv4AddressLexerFactory.Create();
            var e = Alternation.Create(c, ipv4Lexer);

            // ls32
            return new LeastSignificantInt32Lexer(e);
        }
    }
}
