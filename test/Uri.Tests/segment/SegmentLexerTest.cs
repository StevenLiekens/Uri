using Txt.Core;
using Xunit;

namespace UriSyntax.segment
{
    public class SegmentLexerTest : LexerTestBase
    {
        [Theory]
        [InlineData(@"")]
        [InlineData(@"@")]
        [InlineData(@":")]
        [InlineData(@":@")]
        [InlineData(@"@:")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = Container.GetInstance<ILexer<Segment>>();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
