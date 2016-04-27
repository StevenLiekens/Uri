using Xunit;

namespace Uri
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
