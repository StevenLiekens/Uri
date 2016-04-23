using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;
using Xunit;

namespace Uri.unreserved
{
    public class UnreservedLexerTests
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
            var terminalLexerFactory = new TerminalLexerFactory();
            var alternationLexerFactory = new AlternationLexerFactory();
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var alphaLexerFactory = new AlphaLexerFactory(valueRangeLexerFactory, alternationLexerFactory);
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var factory = new UnreservedLexerFactory(alphaLexerFactory, digitLexerFactory, terminalLexerFactory, alternationLexerFactory);
            var lexer = factory.Create();
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
