﻿using System.Diagnostics;
using Txt.ABNF;
using Uri.hier_part;
using Uri.query;
using Uri.scheme;

namespace Uri.absolute_URI
{
    public class AbsoluteUri : Concatenation
    {
        public AbsoluteUri(Concatenation concatenation) 
            : base(concatenation)
        {
        }

        public HierarchicalPart HierarchicalPart
        {
            get
            {
                Debug.Assert(Elements[2] is HierarchicalPart, "this.Elements[2] is HierarchicalPart");
                return (HierarchicalPart)Elements[2];
            }
        }

        public Query Query
        {
            get
            {
                var optionalQueryPart = (Repetition)Elements[3];
                if (optionalQueryPart.Elements.Count == 0)
                {
                    return null;
                }

                var queryPart = (Concatenation)optionalQueryPart.Elements[0];
                return (Query)queryPart.Elements[1];
            }
        }

        public Scheme Scheme
        {
            get
            {
                Debug.Assert(Elements[0] is Scheme, "this.Elements[0] is Scheme");
                return (Scheme)Elements[0];
            }
        }
    }
}