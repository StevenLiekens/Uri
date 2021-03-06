﻿using Txt.Core;
using Xunit;

namespace UriSyntax.IPv6address
{
    public class IPV6AddressLexerTest
    {
        [Theory]

        // 6( h16 ":" ) ls32
        [InlineData(@"1:2:3:4:5:6:7:8")]

        // "::" 5( h16 ":" ) ls32
        [InlineData(@"::1:2:3:4:5:6:7")]

        // [ h16 ] "::" 4( h16 ":" ) ls32
        [InlineData(@"::1:2:3:4:5:6")]
        [InlineData(@"1::2:3:4:5:6:7")]

        // [ *1( h16 ":" ) h16 ] "::" 3( h16 ":" ) ls32
        [InlineData(@"::1:2:3:4:5")]
        [InlineData(@"1::2:3:4:5:6")]
        [InlineData(@"1:2::3:4:5:6:7")]

        // [ *2( h16 ":" ) h16 ] "::" 2( h16 ":" ) ls32
        [InlineData(@"::1:2:3:4")]
        [InlineData(@"1::2:3:4:5")]
        [InlineData(@"1:2::3:4:5:6")]
        [InlineData(@"1:2:3::4:5:6:7")]

        // [ *3( h16 ":" ) h16 ] "::"    h16 ":"   ls32
        [InlineData(@"::1:2:3")]
        [InlineData(@"1::2:3:4")]
        [InlineData(@"1:2::3:4:5")]
        [InlineData(@"1:2:3::4:5:6")]
        [InlineData(@"1:2:3:4::5:6:7")]

        // [ *4( h16 ":" ) h16 ] "::" ls32
        [InlineData(@"::1:2")]
        [InlineData(@"1::2:3")]
        [InlineData(@"1:2::3:4")]
        [InlineData(@"1:2:3::4:5")]
        [InlineData(@"1:2:3:4::5:6")]
        [InlineData(@"1:2:3:4:5::6:7")]

        // [ *5( h16 ":" ) h16 ] "::" h16
        [InlineData(@"::1")]
        [InlineData(@"1::2")]
        [InlineData(@"1:2::3")]
        [InlineData(@"1:2:3::4")]
        [InlineData(@"1:2:3:4::5")]
        [InlineData(@"1:2:3:4:5::6")]
        [InlineData(@"1:2:3:4:5:6::7")]

        //  [ *6( h16 ":" ) h16 ] "::"
        [InlineData(@"::")]
        [InlineData(@"1::")]
        [InlineData(@"1:2::")]
        [InlineData(@"1:2:3::")]
        [InlineData(@"1:2:3:4::")]
        [InlineData(@"1:2:3:4:5::")]
        [InlineData(@"1:2:3:4:5:6::")]
        [InlineData(@"1:2:3:4:5:6:7::")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = IPv6AddressLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
