using Txt.Core;
using Xunit;

namespace UriSyntax.reserved
{
    public class ReservedLexerTest
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
            var lexer = ReservedLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
