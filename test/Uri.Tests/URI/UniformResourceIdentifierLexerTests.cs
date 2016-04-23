using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;
using Txt.ABNF.Core.HEXDIG;
using Uri.authority;
using Uri.dec_octet;
using Uri.fragment;
using Uri.h16;
using Uri.hier_part;
using Uri.host;
using Uri.IPv4address;
using Uri.IPv6address;
using Uri.IPvFuture;
using Uri.IP_literal;
using Uri.ls32;
using Uri.path_abempty;
using Uri.path_absolute;
using Uri.path_empty;
using Uri.path_rootless;
using Uri.pchar;
using Uri.pct_encoded;
using Uri.port;
using Uri.query;
using Uri.reg_name;
using Uri.scheme;
using Uri.segment;
using Uri.segment_nz;
using Uri.sub_delims;
using Uri.unreserved;
using Uri.userinfo;
using Xunit;

namespace Uri.URI
{
    public class UniformResourceIdentifierLexerTests
    {
        [Theory]
        [InlineData(@"ftp://ftp.is.co.za/rfc/rfc1808.txt")]
        [InlineData(@"http://www.ietf.org/rfc/rfc2396.txt")]
        [InlineData(@"ldap://[2001:db8::7]/c=GB?objectClass?one")]
        [InlineData(@"mailto:John.Doe@example.com")]
        [InlineData(@"news:comp.infosystems.www.servers.unix")]
        [InlineData(@"tel:+1-816-555-1212")]
        [InlineData(@"telnet://192.0.2.16:80/")]
        [InlineData(@"urn:oasis:names:specification:docbook:dtd:xml:4.1.2")]
        public void Read_ShouldSucceed(string input)
        {
            var concatenationLexerFactory = new ConcatenationLexerFactory();
            var optionLexerFactory = new OptionLexerFactory();
            var terminalLexerFactory = new TerminalLexerFactory();
            var alternationLexerFactory = new AlternationLexerFactory();
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var alphaLexerFactory = new AlphaLexerFactory(valueRangeLexerFactory, alternationLexerFactory);
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var schemeLexerFactory = new SchemeLexerFactory(
                concatenationLexerFactory,
                alternationLexerFactory,
                repetitionLexerFactory,
                alphaLexerFactory,
                digitLexerFactory,
                terminalLexerFactory);
            var unreservedLexerFactory = new UnreservedLexerFactory(
                alphaLexerFactory,
                digitLexerFactory,
                terminalLexerFactory,
                alternationLexerFactory);
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(
                digitLexerFactory,
                terminalLexerFactory,
                alternationLexerFactory);
            var percentEncodingLexerFactory = new PercentEncodingLexerFactory(
                terminalLexerFactory,
                hexadecimalDigitLexerFactory,
                concatenationLexerFactory);
            var subcomponentsDelimiterLexerFactory = new SubcomponentsDelimiterLexerFactory(
                terminalLexerFactory,
                alternationLexerFactory);
            var userInformationLexerFactory = new UserInformationLexerFactory(
                repetitionLexerFactory,
                alternationLexerFactory,
                terminalLexerFactory,
                unreservedLexerFactory,
                percentEncodingLexerFactory,
                subcomponentsDelimiterLexerFactory);
            var hexadecimalInt16LexerFactory = new HexadecimalInt16LexerFactory(
                repetitionLexerFactory,
                hexadecimalDigitLexerFactory);
            var decimalOctetLexerFactory = new DecimalOctetLexerFactory(
                valueRangeLexerFactory,
                terminalLexerFactory,
                alternationLexerFactory,
                repetitionLexerFactory,
                digitLexerFactory,
                concatenationLexerFactory);
            var ipv4AddressLexerFactory = new IPv4AddressLexerFactory(
                concatenationLexerFactory,
                terminalLexerFactory,
                decimalOctetLexerFactory);
            var leastSignificantInt32LexerFactory = new LeastSignificantInt32LexerFactory(
                alternationLexerFactory,
                concatenationLexerFactory,
                terminalLexerFactory,
                hexadecimalInt16LexerFactory,
                ipv4AddressLexerFactory);
            var ipv6AddressLexerFactory = new IPv6AddressLexerFactory(
                alternationLexerFactory,
                concatenationLexerFactory,
                terminalLexerFactory,
                repetitionLexerFactory,
                optionLexerFactory,
                hexadecimalInt16LexerFactory,
                leastSignificantInt32LexerFactory);
            var ipvFutureLexerFactory = new IPvFutureLexerFactory(
                terminalLexerFactory,
                repetitionLexerFactory,
                concatenationLexerFactory,
                alternationLexerFactory,
                hexadecimalDigitLexerFactory,
                unreservedLexerFactory,
                subcomponentsDelimiterLexerFactory);
            var ipLiteralLexerFactory = new IPLiteralLexerFactory(
                concatenationLexerFactory,
                alternationLexerFactory,
                terminalLexerFactory,
                ipv6AddressLexerFactory,
                ipvFutureLexerFactory);
            var encodingLexerFactory = new PercentEncodingLexerFactory(
                terminalLexerFactory,
                hexadecimalDigitLexerFactory,
                concatenationLexerFactory);
            var registeredNameLexerFactory = new RegisteredNameLexerFactory(
                repetitionLexerFactory,
                alternationLexerFactory,
                unreservedLexerFactory,
                encodingLexerFactory,
                subcomponentsDelimiterLexerFactory);
            var hostLexerFactory = new HostLexerFactory(
                alternationLexerFactory,
                ipLiteralLexerFactory,
                ipv4AddressLexerFactory,
                registeredNameLexerFactory);
            var portLexerFactory = new PortLexerFactory(repetitionLexerFactory, digitLexerFactory);
            var authorityLexerFactory = new AuthorityLexerFactory(
                optionLexerFactory,
                concatenationLexerFactory,
                userInformationLexerFactory,
                terminalLexerFactory,
                hostLexerFactory,
                portLexerFactory);
            var pathCharacterLexerFactory = new PathCharacterLexerFactory(
                unreservedLexerFactory,
                percentEncodingLexerFactory,
                subcomponentsDelimiterLexerFactory,
                terminalLexerFactory,
                alternationLexerFactory);
            var segmentLexerFactory = new SegmentLexerFactory(pathCharacterLexerFactory, repetitionLexerFactory);
            var segmentNonZeroLengthLexerFactory = new SegmentNonZeroLengthLexerFactory(
                pathCharacterLexerFactory,
                repetitionLexerFactory);
            var pathAbsoluteLexerFactory = new PathAbsoluteLexerFactory(
                terminalLexerFactory,
                optionLexerFactory,
                concatenationLexerFactory,
                repetitionLexerFactory,
                segmentLexerFactory,
                segmentNonZeroLengthLexerFactory);
            var pathAbsoluteOrEmptyLexerFactory = new PathAbsoluteOrEmptyLexerFactory(
                repetitionLexerFactory,
                concatenationLexerFactory,
                terminalLexerFactory,
                segmentLexerFactory);
            var pathEmptyLexerFactory = new PathEmptyLexerFactory(terminalLexerFactory);
            var pathRootlessLexerFactory = new PathRootlessLexerFactory(
                concatenationLexerFactory,
                repetitionLexerFactory,
                terminalLexerFactory,
                segmentLexerFactory,
                segmentNonZeroLengthLexerFactory);
            var hierarchicalPartLexerFactory = new HierarchicalPartLexerFactory(
                alternationLexerFactory,
                authorityLexerFactory,
                pathAbsoluteLexerFactory,
                pathAbsoluteOrEmptyLexerFactory,
                pathEmptyLexerFactory,
                pathRootlessLexerFactory,
                concatenationLexerFactory,
                terminalLexerFactory);
            var queryLexerFactory = new QueryLexerFactory(
                alternationLexerFactory,
                pathCharacterLexerFactory,
                repetitionLexerFactory,
                terminalLexerFactory);
            var fragmentLexerFactory = new FragmentLexerFactory(
                alternationLexerFactory,
                pathCharacterLexerFactory,
                repetitionLexerFactory,
                terminalLexerFactory);
            var factory = new UniformResourceIdentifierLexerFactory(
                concatenationLexerFactory,
                optionLexerFactory,
                terminalLexerFactory,
                schemeLexerFactory,
                hierarchicalPartLexerFactory,
                queryLexerFactory,
                fragmentLexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}