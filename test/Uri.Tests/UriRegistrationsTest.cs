using Xunit;

namespace UriSyntax
{
    public class UriRegistrationsTest : LexerTestBase
    {
        [Fact]
        public void VerifyRegistrations()
        {
            Container.Verify();
        }
    }
}
