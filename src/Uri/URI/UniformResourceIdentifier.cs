using System.Diagnostics;
using Txt.ABNF;
using UriSyntax.fragment;
using UriSyntax.hier_part;
using UriSyntax.query;
using UriSyntax.scheme;

namespace UriSyntax.URI
{
    /// <summary>Represents a Uniform Resource Identifier (URI) as described in RFC 3986.</summary>
    /// <remarks>See: <a href="https://www.ietf.org/rfc/rfc3986.txt">RFC 3986</a>.</remarks>
    public class UniformResourceIdentifier : Concatenation
    {
        public UniformResourceIdentifier(Concatenation concatenation)
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
