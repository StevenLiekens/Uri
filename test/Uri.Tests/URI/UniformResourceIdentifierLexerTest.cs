using Txt;
using Xunit;

namespace Uri.URI
{
    public class UniformResourceIdentifierLexerTest : LexerTestBase
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
            var lexer = Container.GetInstance<ILexer<UniformResourceIdentifier>>();
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
