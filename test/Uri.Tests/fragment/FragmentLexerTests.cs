using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;
using Txt.ABNF.Core.HEXDIG;
using Uri.pchar;
using Uri.pct_encoded;
using Uri.sub_delims;
using Uri.unreserved;
using Xunit;

namespace Uri.fragment
{
    public class FragmentLexerTests
    {
        [Theory]
        [InlineData(@":")]
        [InlineData(@"@")]
        [InlineData(@"/")]
        [InlineData(@"?")]
        public void Read_ShouldSucceed(string input)
        {
            var terminalLexerFactory = new TerminalLexerFactory();
            var alternationLexerFactory = new AlternationLexerFactory();
            var subcomponentsDelimiterLexerFactory = new SubcomponentsDelimiterLexerFactory(
                terminalLexerFactory,
                alternationLexerFactory);
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var concatenationLexerFactory = new ConcatenationLexerFactory();
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(
                digitLexerFactory,
                terminalLexerFactory,
                alternationLexerFactory);
            var alphaLexerFactory = new AlphaLexerFactory(valueRangeLexerFactory, alternationLexerFactory);
            var percentEncodingLexerFactory = new PercentEncodingLexerFactory(
                terminalLexerFactory,
                hexadecimalDigitLexerFactory,
                concatenationLexerFactory);
            var unreservedLexerFactory = new UnreservedLexerFactory(
                alphaLexerFactory,
                digitLexerFactory,
                terminalLexerFactory,
                alternationLexerFactory);
            var pathCharacterLexerFactory = new PathCharacterLexerFactory(
                unreservedLexerFactory,
                percentEncodingLexerFactory,
                subcomponentsDelimiterLexerFactory,
                terminalLexerFactory,
                alternationLexerFactory);
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var factory = new FragmentLexerFactory(
                alternationLexerFactory,
                pathCharacterLexerFactory,
                repetitionLexerFactory,
                terminalLexerFactory);
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
