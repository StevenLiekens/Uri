using Txt.ABNF;

namespace UriSyntax.port
{
    public class Port : Repetition
    {
        public Port(Repetition repetition)
            : base(repetition)
        {
        }
    }
}
