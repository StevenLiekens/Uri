using Txt.ABNF;
using Uri.authority;
using Uri.path_abempty;
using Uri.path_absolute;
using Uri.path_empty;
using Uri.path_rootless;

namespace Uri.hier_part
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
    }
}