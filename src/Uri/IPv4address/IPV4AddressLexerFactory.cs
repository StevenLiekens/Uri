using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.dec_octet;

namespace UriSyntax.IPv4address
{
    public class IPv4AddressLexerFactory : ILexerFactory<IPv4Address>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<DecimalOctet> decimaOctetLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public IPv4AddressLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<DecimalOctet> decimaOctetLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (decimaOctetLexer == null)
            {
                throw new ArgumentNullException(nameof(decimaOctetLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.decimaOctetLexer = decimaOctetLexer;
        }

        public ILexer<IPv4Address> Create()
        {
            // dec-octet
            var a = decimaOctetLexer;

            // "."
            var b = terminalLexerFactory.Create(@".", StringComparer.Ordinal);

            // dec-octet "." dec-octet "." dec-octet "." dec-octet
            var innerLexer = concatenationLexerFactory.Create(a, b, a, b, a, b, a);

            // IPv4address
            return new IPv4AddressLexer(innerLexer);
        }
    }
}
