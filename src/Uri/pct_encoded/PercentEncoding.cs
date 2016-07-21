using System;
using System.Diagnostics;
using Txt.ABNF;

namespace UriSyntax.pct_encoded
{
    public class PercentEncoding : Concatenation
    {
        public PercentEncoding(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public static explicit operator char(PercentEncoding instance)
        {
            return instance.ToChar();
        }

        public char ToChar()
        {
            Debug.Assert(Text != null, "this.Value != null");
            Debug.Assert(Text.Length == 3, "this.Value.Length == 3");
            return (char)Convert.ToInt32(Text.Substring(1), 16);
        }
    }
}
