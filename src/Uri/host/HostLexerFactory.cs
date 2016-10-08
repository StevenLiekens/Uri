using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.IPv4address;
using UriSyntax.IP_literal;
using UriSyntax.reg_name;

namespace UriSyntax.host
{
    public class HostLexerFactory : LexerFactory<Host>
    {
        static HostLexerFactory()
        {
            Default = new HostLexerFactory(
                Txt.ABNF.AlternationLexerFactory.Default,
                IPLiteralLexerFactory.Default.Singleton(),
                IPv4AddressLexerFactory.Default.Singleton(),
                reg_name.RegisteredNameLexerFactory.Default.Singleton());
        }

        public HostLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexerFactory<IPLiteral> ipLiteralLexerFactory,
            [NotNull] ILexerFactory<IPv4Address> ipv4AddressLexerFactory,
            [NotNull] ILexerFactory<RegisteredName> registeredNameLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (ipLiteralLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipLiteralLexerFactory));
            }
            if (ipv4AddressLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipv4AddressLexerFactory));
            }
            if (registeredNameLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(registeredNameLexerFactory));
            }
            AlternationLexerFactory = alternationLexerFactory;
            IpLiteralLexerFactory = ipLiteralLexerFactory;
            Ipv4AddressLexerFactory = ipv4AddressLexerFactory;
            RegisteredNameLexerFactory = registeredNameLexerFactory;
        }

        public static HostLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public ILexerFactory<IPLiteral> IpLiteralLexerFactory { get; }

        public ILexerFactory<IPv4Address> Ipv4AddressLexerFactory { get; }

        public ILexerFactory<RegisteredName> RegisteredNameLexerFactory { get; }

        public override ILexer<Host> Create()
        {
            var innerLexer = AlternationLexerFactory.Create(
                IpLiteralLexerFactory.Create(),
                Ipv4AddressLexerFactory.Create(),
                RegisteredNameLexerFactory.Create());
            return new HostLexer(innerLexer);
        }
    }
}
