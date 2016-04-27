using Txt;
using Xunit;

namespace Uri.path_empty
{
    public class PathEmptyLexerTest : LexerTestBase
    {
        [Theory]
        [InlineData(@"")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = Container.GetInstance<ILexer<PathEmpty>>();
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
