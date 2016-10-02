using Txt.Core;
using Xunit;

namespace UriSyntax.gen_delims
{
    public class GenericDelimiterLexerTest
    {
        [Theory]
        [InlineData(@":")]
        [InlineData(@"/")]
        [InlineData(@"?")]
        [InlineData(@"#")]
        [InlineData(@"[")]
        [InlineData(@"]")]
        [InlineData(@"@")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = GenericDelimiterLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
