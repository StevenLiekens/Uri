using Txt.Core;
using Xunit;

namespace Uri.unreserved
{
    public class UnreservedLexerTest : LexerTestBase
    {
        [Theory]
        [InlineData(@"a")]
        [InlineData(@"B")]
        [InlineData(@"c")]
        [InlineData(@"0")]
        [InlineData(@"1")]
        [InlineData(@"2")]
        [InlineData(@"-")]
        [InlineData(@".")]
        [InlineData(@"_")]
        [InlineData(@"~")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = Container.GetInstance<ILexer<Unreserved>>();
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
