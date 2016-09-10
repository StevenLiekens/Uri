using Txt.Core;
using Xunit;

namespace UriSyntax.pchar
{
    public class PathCharacterLexerTest : LexerTestBase
    {
        [Theory]
        [InlineData(@":")]
        [InlineData(@"@")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = Container.GetInstance<ILexer<PathCharacter>>();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
