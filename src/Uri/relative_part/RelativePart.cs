using Txt.ABNF;
using Uri.authority;
using Uri.path_abempty;
using Uri.path_absolute;
using Uri.path_empty;
using Uri.path_noscheme;

namespace Uri.relative_part
{
    public class RelativePart : Alternation
    {
        public RelativePart(Alternation alternation)
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

        public PathNoScheme PathNoScheme
        {
            get
            {
                if (Ordinal != 3)
                {
                    return null;
                }

                return (PathNoScheme)Element;
            }
        }
    }
}