using Txt.Core;
using Xunit;

namespace UriSyntax.reserved
{
    public class ReservedLexerTest : LexerTestBase
    {
        [Theory]
        [InlineData(@"!")]
        [InlineData(@"$")]
        [InlineData(@"&")]
        [InlineData(@"'")]
        [InlineData(@"(")]
        [InlineData(@")")]
        [InlineData(@"*")]
        [InlineData(@"+")]
        [InlineData(@",")]
        [InlineData(@";")]
        [InlineData(@"=")]
        [InlineData(@"!")]
        [InlineData(@"$")]
        [InlineData(@"&")]
        [InlineData(@"'")]
        [InlineData(@"(")]
        [InlineData(@")")]
        [InlineData(@"*")]
        [InlineData(@"+")]
        [InlineData(@",")]
        [InlineData(@";")]
        [InlineData(@"=")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = Container.GetInstance<ILexer<Reserved>>();
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
