using System;
using Txt;
using Txt.ABNF;
using Uri.IPv6address;

namespace Uri.IP_literal
{
    public class IPLiteralLexerFactory : ILexerFactory<IPLiteral>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexerFactory<IPv6Address> ipv6AddressLexerFactory;

        private readonly ILexerFactory<IPvFuture.IPvFuture> ipvFutureLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public IPLiteralLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<IPv6Address> ipv6AddressLexerFactory,
            ILexerFactory<IPvFuture.IPvFuture> ipvFutureLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (ipv6AddressLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipv6AddressLexerFactory));
            }

            if (ipvFutureLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipvFutureLexerFactory));
            }

            this.concatenationLexerFactory = concatenationLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.ipv6AddressLexerFactory = ipv6AddressLexerFactory;
            this.ipvFutureLexerFactory = ipvFutureLexerFactory;
        }

        public ILexer<IPLiteral> Create()
        {
            var a = terminalLexerFactory.Create(@"[", StringComparer.Ordinal);
            var b = terminalLexerFactory.Create(@"]", StringComparer.Ordinal);
            var ipv6 = ipv6AddressLexerFactory.Create();
            var ipvFuture = ipvFutureLexerFactory.Create();
            var alt = alternationLexerFactory.Create(ipv6, ipvFuture);
            var innerLexer = concatenationLexerFactory.Create(a, alt, b);
            return new IPLiteralLexer(innerLexer);
        }
    }
}