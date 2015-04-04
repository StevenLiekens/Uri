﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uri.Grammar
{
    using System.IO;
    using System.Text;

    using SLANG;

    

    [TestClass]
    public class HexadecimalInt16LexerTests
    {
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "h16.xml", "Row", DataAccessMethod.Sequential)]
        public void ReadHexadecimalInt16()
        {
            var dataRow = this.TestContext.DataRow;
            var input = (string)dataRow["Input"];
            var expected = (string)dataRow["Expected"];
            var lexer = new HexadecimalInt16Lexer();
            using (var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(input)))
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.AreEqual(expected, element.Data);
            }
        }

        public TestContext TestContext { get; set; }
    }
}
