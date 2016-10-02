using Txt.Core;
using Xunit;

namespace UriSyntax.segment_nz_nc
{
    public class SegmentNonZeroLengthNoColonsLexerTest
    {
        [Theory]
        [InlineData(@"@")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = SegmentNonZeroLengthNoColonsLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
