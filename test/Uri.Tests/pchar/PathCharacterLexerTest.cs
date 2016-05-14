using Txt.Core;
using Xunit;

namespace Uri.pchar
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
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}
