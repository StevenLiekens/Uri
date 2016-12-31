using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.dec_octet;

namespace UriSyntax.IPv4address
{
    // ReSharper disable once InconsistentNaming
    public class IPv4AddressLexerFactory : RuleLexerFactory<IPv4Address>
    {
        static IPv4AddressLexerFactory()
        {
            Default = new IPv4AddressLexerFactory(DecimalOctetLexerFactory.Default.Singleton());
        }

        public IPv4AddressLexerFactory(
            [NotNull] ILexerFactory<DecimalOctet> decimaOctetLexerFactory)
        {
            if (decimaOctetLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(decimaOctetLexerFactory));
            }
            DecimaOctetLexerFactory = decimaOctetLexerFactory;
        }

        [NotNull]
        public static IPv4AddressLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<DecimalOctet> DecimaOctetLexerFactory { get; }

        public override ILexer<IPv4Address> Create()
        {
            // dec-octet
            var a = DecimaOctetLexerFactory.Create();

            // "."
            var b = Terminal.Create(@".", StringComparer.Ordinal);

            // dec-octet "." dec-octet "." dec-octet "." dec-octet
            var innerLexer = Concatenation.Create(a, b, a, b, a, b, a);

            // IPv4address
            return new IPv4AddressLexer(innerLexer);
        }
    }
}
