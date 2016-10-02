using Txt.Core;
using Xunit;

namespace UriSyntax.pchar
{
    public class PathCharacterLexerTest
    {
        [Theory]
        [InlineData(@":")]
        [InlineData(@"@")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = PathCharacterLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
