using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.dec_octet;

namespace UriSyntax.IPv4address
{
    public class IPv4AddressLexerFactory : LexerFactory<IPv4Address>
    {
        static IPv4AddressLexerFactory()
        {
            Default = new IPv4AddressLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                DecimalOctetLexerFactory.Default.Singleton());
        }

        public IPv4AddressLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexerFactory<DecimalOctet> decimaOctetLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (decimaOctetLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(decimaOctetLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            DecimaOctetLexerFactory = decimaOctetLexerFactory;
        }

        public static IPv4AddressLexerFactory Default { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<DecimalOctet> DecimaOctetLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<IPv4Address> Create()
        {
            // dec-octet
            var a = DecimaOctetLexerFactory.Create();

            // "."
            var b = TerminalLexerFactory.Create(@".", StringComparer.Ordinal);

            // dec-octet "." dec-octet "." dec-octet "." dec-octet
            var innerLexer = ConcatenationLexerFactory.Create(a, b, a, b, a, b, a);

            // IPv4address
            return new IPv4AddressLexer(innerLexer);
        }
    }
}
