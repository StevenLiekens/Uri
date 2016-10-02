using Txt.Core;
using Xunit;

namespace UriSyntax.segment_nz
{
    public class SegmentNonZeroLengthLexerTest
    {
        [Theory]
        [InlineData(@":@")]
        [InlineData(@"@:")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = SegmentNonZeroLengthLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
