using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.IPv4address;
using Uri.IP_literal;
using Uri.reg_name;

namespace Uri.host
{
    public class HostLexerFactory : ILexerFactory<Host>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<IPLiteral> ipLiteralLexer;

        private readonly ILexer<IPv4Address> ipv4AddressLexer;

        private readonly ILexer<RegisteredName> registeredNameLexer;

        public HostLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<IPLiteral> ipLiteralLexer,
            [NotNull] ILexer<IPv4Address> ipv4AddressLexer,
            [NotNull] ILexer<RegisteredName> registeredNameLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (ipLiteralLexer == null)
            {
                throw new ArgumentNullException(nameof(ipLiteralLexer));
            }
            if (ipv4AddressLexer == null)
            {
                throw new ArgumentNullException(nameof(ipv4AddressLexer));
            }
            if (registeredNameLexer == null)
            {
                throw new ArgumentNullException(nameof(registeredNameLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.ipLiteralLexer = ipLiteralLexer;
            this.ipv4AddressLexer = ipv4AddressLexer;
            this.registeredNameLexer = registeredNameLexer;
        }

        public ILexer<Host> Create()
        {
            var innerLexer = alternationLexerFactory.Create(ipLiteralLexer, ipv4AddressLexer, registeredNameLexer);
            return new HostLexer(innerLexer);
        }
    }
}
