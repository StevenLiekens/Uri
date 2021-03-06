﻿using Txt.Core;
using Xunit;

namespace UriSyntax.IPv4address
{
    public class IPV4AddressLexerTest
    {
        [Theory]
        [InlineData(@"0.0.0.0")]
        [InlineData(@"10.0.0.0")]
        [InlineData(@"127.0.0.0")]
        [InlineData(@"169.254.0.0")]
        [InlineData(@"172.16.0.0")]
        [InlineData(@"192.0.0.0")]
        [InlineData(@"192.0.2.0")]
        [InlineData(@"192.88.99.0")]
        [InlineData(@"192.168.0.0")]
        [InlineData(@"198.18.0.0")]
        [InlineData(@"198.51.100.0")]
        [InlineData(@"203.0.113.0")]
        [InlineData(@"224.0.0.0")]
        [InlineData(@"240.0.0.0")]
        [InlineData(@"255.255.255.255")]
        public void Read_ShouldSucceed(string input)
        {
            var lexer = IPv4AddressLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
