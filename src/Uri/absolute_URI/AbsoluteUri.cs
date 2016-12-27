using System.Diagnostics;
using JetBrains.Annotations;
using Txt.ABNF;
using UriSyntax.hier_part;
using UriSyntax.query;
using UriSyntax.scheme;

namespace UriSyntax.absolute_URI
{
    public class AbsoluteUri : Concatenation
    {
        public AbsoluteUri([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }

        public HierarchicalPart HierarchicalPart
        {
            get
            {
                Debug.Assert(this[2] is HierarchicalPart, "this[2] is HierarchicalPart");
                return (HierarchicalPart)this[2];
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

        public Scheme Scheme
        {
            get
            {
                Debug.Assert(this[0] is Scheme, "this[0] is Scheme");
                return (Scheme)this[0];
            }
        }
    }
}
