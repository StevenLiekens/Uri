using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.IPv4address;
using UriSyntax.IP_literal;
using UriSyntax.reg_name;

namespace UriSyntax.host
{
    public class HostLexerFactory : RuleLexerFactory<Host>
    {
        static HostLexerFactory()
        {
            Default = new HostLexerFactory(
                IP_literal.IPLiteralLexerFactory.Default.Singleton(),
                IPv4address.IPv4AddressLexerFactory.Default.Singleton(),
                reg_name.RegisteredNameLexerFactory.Default.Singleton());
        }

        public HostLexerFactory(
            [NotNull] ILexerFactory<IPLiteral> ipLiteralLexerFactory,
            [NotNull] ILexerFactory<IPv4Address> ipv4AddressLexerFactory,
            [NotNull] ILexerFactory<RegisteredName> registeredNameLexerFactory)
        {
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
            IPLiteralLexerFactory = ipLiteralLexerFactory;
            IPv4AddressLexerFactory = ipv4AddressLexerFactory;
            RegisteredNameLexerFactory = registeredNameLexerFactory;
        }

        [NotNull]
        public static HostLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<IPLiteral> IPLiteralLexerFactory { get; }

        [NotNull]
        // ReSharper disable once InconsistentNaming
        public ILexerFactory<IPv4Address> IPv4AddressLexerFactory { get; }

        [NotNull]
        public ILexerFactory<RegisteredName> RegisteredNameLexerFactory { get; }

        public override ILexer<Host> Create()
        {
            var innerLexer = Alternation.Create(
                IPLiteralLexerFactory.Create(),
                IPv4AddressLexerFactory.Create(),
                RegisteredNameLexerFactory.Create());
            return new HostLexer(innerLexer);
        }
    }
}
