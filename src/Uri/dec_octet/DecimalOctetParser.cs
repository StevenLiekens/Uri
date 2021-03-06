﻿using System.Globalization;
using Txt.Core;

namespace UriSyntax.dec_octet
{
    public class DecimalOctetParser : Parser<DecimalOctet, byte>
    {
        protected override byte ParseImpl(DecimalOctet value)
        {
            return byte.Parse(value.Text, NumberStyles.None, NumberFormatInfo.InvariantInfo);
        }
    }
}
