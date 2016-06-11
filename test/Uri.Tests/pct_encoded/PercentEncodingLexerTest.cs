using Txt.Core;
using Xunit;

namespace UriSyntax.pct_encoded
{
    public class PercentEncodingLexerTest : LexerTestBase
    {
        [Theory]
        [InlineData(@"%00")]
        [InlineData(@"%FF")]
        [InlineData(@"%20")]
        [InlineData(@"%99")]
        [InlineData(@"%AA")]
        [InlineData(@"%01")]
        [InlineData(@"%10")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = Container.GetInstance<ILexer<PercentEncoding>>();
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
