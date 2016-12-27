using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.IPv6address;
using UriSyntax.IPvFuture;

namespace UriSyntax.IP_literal
{
    public class IPLiteralLexerFactory : LexerFactory<IPLiteral>
    {
        static IPLiteralLexerFactory()
        {
            Default = new IPLiteralLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                IPv6AddressLexerFactory.Default.Singleton(),
                IPvFutureLexerFactory.Default.Singleton());
        }

        public IPLiteralLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexerFactory<IPv6Address> ipv6AddressLexerFactory,
            [NotNull] ILexerFactory<IPvFuture.IPvFuture> ipvFutureLexerFactory)
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
            if (ipv6AddressLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipv6AddressLexerFactory));
            }
            if (ipvFutureLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipvFutureLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            Ipv6AddressLexerFactory = ipv6AddressLexerFactory;
            IpvFutureLexerFactory = ipvFutureLexerFactory;
        }

        [NotNull]
        public static IPLiteralLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<IPv6Address> Ipv6AddressLexerFactory { get; }

        [NotNull]
        public ILexerFactory<IPvFuture.IPvFuture> IpvFutureLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<IPLiteral> Create()
        {
            var a = TerminalLexerFactory.Create(@"[", StringComparer.Ordinal);
            var b = TerminalLexerFactory.Create(@"]", StringComparer.Ordinal);
            var alt = AlternationLexerFactory.Create(Ipv6AddressLexerFactory.Create(), IpvFutureLexerFactory.Create());
            var innerLexer = ConcatenationLexerFactory.Create(a, alt, b);
            return new IPLiteralLexer(innerLexer);
        }
    }
}
