using System.Diagnostics;
using Txt.ABNF;
using Uri.relative_ref;
using Uri.URI;

namespace Uri.URI_reference
{
    public class UriReference : Alternation
    {
        public UriReference(Alternation alternation)
            : base(alternation)
        {
        }

        public bool IsAbsolute
        {
            get
            {
                Debug.Assert((Ordinal == 1) || (Ordinal == 2), "this.Ordinal == 1 || this.Ordinal == 2");
                if (Ordinal == 1)
                {
                    return true;
                }

                return false;
            }
        }

        public RelativeReference RelativeReference
        {
            get
            {
                if (Ordinal != 2)
                {
                    return null;
                }

                return (RelativeReference)Element;
            }
        }

        public UniformResourceIdentifier UniformResourceIdentifier
        {
            get
            {
                if (Ordinal != 1)
                {
                    return null;
                }

                return (UniformResourceIdentifier)Element;
            }
        }
    }
}