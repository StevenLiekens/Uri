using System.Globalization;
using Txt.Core;

namespace Uri.h16
{
    public class HexadecimalInt16Parser : Parser<HexadecimalInt16, byte[]>
    {
        protected override byte[] ParseImpl(HexadecimalInt16 value)
        {
            var bytes = new byte[2];
            var ix = 1;
            var hex = value.Text;
            for (var i = hex.Length; (i > 0) && (ix >= 0); i -= 2, ix--)
            {
                var subLength = i == 1 ? 1 : 2;
                var substr = hex.Substring(i - subLength, subLength);
                bytes[ix] = byte.Parse(substr, NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo);
            }
            return bytes;
        }
    }
}
