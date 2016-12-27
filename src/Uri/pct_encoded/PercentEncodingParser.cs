using System;
using System.Diagnostics;
using Txt.Core;

namespace UriSyntax.pct_encoded
{
    public class PercentEncodingParser : Parser<PercentEncoding, char>
    {
        protected override char ParseImpl(PercentEncoding value)
        {
            Debug.Assert(value.Text.Length == 3, "value.Value.Length == 3");
            return (char)Convert.ToInt32(value.Text.Substring(1), 16);
        }
    }
}
