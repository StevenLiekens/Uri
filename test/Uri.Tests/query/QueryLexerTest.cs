using Txt.Core;
using Xunit;

namespace UriSyntax.query
{
    public class QueryLexerTest : LexerTestBase
    {
        [Theory]
        [InlineData(@":")]
        [InlineData(@"@")]
        [InlineData(@"/")]
        [InlineData(@"?")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = Container.GetInstance<ILexer<Query>>();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}
