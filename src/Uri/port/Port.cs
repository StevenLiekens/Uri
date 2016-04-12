using System;
using Txt.ABNF;

namespace Uri.port
{
    public class Port : Repetition
    {
        public Port(Repetition repetition)
            : base(repetition)
        {
        }

        public int ToInt()
        {
            return Convert.ToInt32(Text);
        }
    }
}