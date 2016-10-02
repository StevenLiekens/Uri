using Txt.ABNF;
using Txt.Core;
using Xunit;

namespace UriSyntax.fragment
{
    public class FragmentLexerTest
    {
        [Theory]
        [InlineData(@":")]
        [InlineData(@"@")]
        [InlineData(@"/")]
        [InlineData(@"?")]
        public void Read_ShouldSucceed(string input)
        {
            var sut = FragmentLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
