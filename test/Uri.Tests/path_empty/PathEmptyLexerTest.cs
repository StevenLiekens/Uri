using Txt.Core;
using Xunit;

namespace UriSyntax.path_empty
{
    public class PathEmptyLexerTest
    {
        [Theory]
        [InlineData(@"")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = PathEmptyLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
