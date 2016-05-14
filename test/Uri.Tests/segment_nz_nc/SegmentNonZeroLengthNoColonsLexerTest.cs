using Txt.Core;
using Xunit;

namespace Uri.segment_nz_nc
{
    public class SegmentNonZeroLengthNoColonsLexerTest : LexerTestBase
    {
        [Theory]
        [InlineData(@"@")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = Container.GetInstance<ILexer<SegmentNonZeroLengthNoColons>>();
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
