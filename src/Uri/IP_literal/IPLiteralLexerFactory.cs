using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.IPv6address;

namespace UriSyntax.IP_literal
{
    public class IPLiteralLexerFactory : RuleLexerFactory<IPLiteral>
    {
        static IPLiteralLexerFactory()
        {
            Default = new IPLiteralLexerFactory(
                IPv6address.IPv6AddressLexerFactory.Default.Singleton(),
                IPvFuture.IPvFutureLexerFactory.Default.Singleton());
        }

        public IPLiteralLexerFactory(
            [NotNull] ILexerFactory<IPv6Address> ipv6AddressLexerFactory,
            [NotNull] ILexerFactory<IPvFuture.IPvFuture> ipvFutureLexerFactory)
        {
            if (ipv6AddressLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipv6AddressLexerFactory));
            }
            if (ipvFutureLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipvFutureLexerFactory));
            }
            IPv6AddressLexerFactory = ipv6AddressLexerFactory;
            IPvFutureLexerFactory = ipvFutureLexerFactory;
        }

        [NotNull]
        public static IPLiteralLexerFactory Default { get; }

        [NotNull]

        // ReSharper disable once InconsistentNaming
        public ILexerFactory<IPv6Address> IPv6AddressLexerFactory { get; }

        [NotNull]

        // ReSharper disable once InconsistentNaming
        public ILexerFactory<IPvFuture.IPvFuture> IPvFutureLexerFactory { get; }

        public override ILexer<IPLiteral> Create()
        {
            var a = Terminal.Create(@"[", StringComparer.Ordinal);
            var b = Terminal.Create(@"]", StringComparer.Ordinal);
            var alt = Alternation.Create(IPv6AddressLexerFactory.Create(), IPvFutureLexerFactory.Create());
            var innerLexer = Concatenation.Create(a, alt, b);
            return new IPLiteralLexer(innerLexer);
        }
    }
}
