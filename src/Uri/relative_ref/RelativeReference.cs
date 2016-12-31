using System.Diagnostics;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.fragment;
using UriSyntax.query;
using UriSyntax.relative_part;

namespace UriSyntax.relative_ref
{
    public class RelativeReference : Concatenation
    {
        public RelativeReference([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }

        public Fragment Fragment
        {
            get
            {
                var optionalFragmentPart = (Repetition)this[4];
                if (optionalFragmentPart.Count == 0)
                {
                    return null;
                }
                var fragmentPart = (Concatenation)optionalFragmentPart[0];
                return (Fragment)fragmentPart[1];
            }
        }

        public Query Query
        {
            get
            {
                var optionalQueryPart = (Repetition)this[3];
                if (optionalQueryPart.Count == 0)
                {
                    return null;
                }
                var queryPart = (Concatenation)optionalQueryPart[0];
                return (Query)queryPart[1];
            }
        }

        public RelativePart RelativePart
        {
            get
            {
                Debug.Assert(this[0] is RelativePart, "this[0] is RelativePart");
                return (RelativePart)this[0];
            }
        }
    }
}
