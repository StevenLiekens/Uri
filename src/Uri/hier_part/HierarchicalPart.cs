using Txt.ABNF;
using UriSyntax.authority;
using UriSyntax.path_abempty;
using UriSyntax.path_absolute;
using UriSyntax.path_empty;
using UriSyntax.path_rootless;

namespace UriSyntax.hier_part
{
    public class HierarchicalPart : Alternation
    {
        public HierarchicalPart(Alternation alternation)
            : base(alternation)
        {
        }

        public Authority Authority
        {
            get
            {
                if (Ordinal != 1)
                {
                    return null;
                }
                var concatenation = (Concatenation)Element;
                return (Authority)concatenation.Elements[1];
            }
        }

        public PathAbsolute PathAbsolute
        {
            get
            {
                if (Ordinal != 2)
                {
                    return null;
                }
                return (PathAbsolute)Element;
            }
        }

        public PathAbsoluteOrEmpty PathAbsoluteOrEmpty
        {
            get
            {
                if (Ordinal != 1)
                {
                    return null;
                }
                var concatenation = (Concatenation)Element;
                return (PathAbsoluteOrEmpty)concatenation.Elements[2];
            }
        }

        public PathEmpty PathEmpty
        {
            get
            {
                if (Ordinal != 4)
                {
                    return null;
                }
                return (PathEmpty)Element;
            }
        }

        public PathRootless PathRootless
        {
            get
            {
                if (Ordinal != 3)
                {
                    return null;
                }
                return (PathRootless)Element;
            }
        }
    }
}
