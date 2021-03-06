﻿using System.Globalization;
using Txt.Core;

namespace UriSyntax.port
{
    public class PortParser : Parser<Port, int>
    {
        protected override int ParseImpl(Port value)
        {
            return int.Parse(value.Text, NumberStyles.None, NumberFormatInfo.InvariantInfo);
        }
    }
}
