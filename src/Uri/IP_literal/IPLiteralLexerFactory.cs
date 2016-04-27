using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.IPv6address;

namespace Uri.IP_literal
{
    public class IPLiteralLexerFactory : ILexerFactory<IPLiteral>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<IPv6Address> ipv6AddressLexer;

        private readonly ILexer<IPvFuture.IPvFuture> ipvFutureLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public IPLiteralLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<IPv6Address> ipv6AddressLexer,
            [NotNull] ILexer<IPvFuture.IPvFuture> ipvFutureLexer)
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
            if (ipv6AddressLexer == null)
            {
                throw new ArgumentNullException(nameof(ipv6AddressLexer));
            }
            if (ipvFutureLexer == null)
            {
                throw new ArgumentNullException(nameof(ipvFutureLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.ipv6AddressLexer = ipv6AddressLexer;
            this.ipvFutureLexer = ipvFutureLexer;
        }

        public ILexer<IPLiteral> Create()
        {
            var a = terminalLexerFactory.Create(@"[", StringComparer.Ordinal);
            var b = terminalLexerFactory.Create(@"]", StringComparer.Ordinal);
            var alt = alternationLexerFactory.Create(ipv6AddressLexer, ipvFutureLexer);
            var innerLexer = concatenationLexerFactory.Create(a, alt, b);
            return new IPLiteralLexer(innerLexer);
        }
    }
}
