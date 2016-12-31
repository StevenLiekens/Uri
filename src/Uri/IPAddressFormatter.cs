using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace UriSyntax
{
    public static class IPAddressFormatter
    {
        private static string FormatHex(int value)
        {
            return $"{value:x}";
        }

        [UsedImplicitly]
        private static string FormatIPv6([NotNull] byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            var segments = new int[8];
            for (var i = 0; i < bytes.Length; i += 2)
            {
                segments[i / 2] = (bytes[i] << 8) | bytes[i + 1];
            }
            Subset emptySubset = null;
            var emptySubsets = new List<Subset>();
            for (var i = 0; i < segments.Length; i++)
            {
                if (segments[i] == ushort.MinValue)
                {
                    if (emptySubset == null)
                    {
                        emptySubset = new Subset { StartIndex = i };
                    }
                    emptySubset.Length += 1;
                }
                else
                {
                    if (emptySubset != null)
                    {
                        emptySubsets.Add(emptySubset);
                        emptySubset = null;
                    }
                }
            }
            if (emptySubset != null)
            {
                emptySubsets.Add(emptySubset);
            }
            if (emptySubsets.Count == 0)
            {
                return string.Join(":", segments.Select(FormatHex));
            }
            var collapse = emptySubsets[0];
            Debug.Assert(collapse != null, "collapse != null");
            if (emptySubsets.Count != 1)
            {
                for (var i = 1; i < emptySubsets.Count; i++)
                {
                    var subset = emptySubsets[i];
                    Debug.Assert(subset != null, "subset != null");
                    if (subset.Length > collapse.Length)
                    {
                        collapse = subset;
                    }
                }
            }
            var buffer = new StringBuilder(39);
            for (var i = 0; i < collapse.StartIndex; i++)
            {
                buffer.AppendFormat("{0:x}:", segments[i]);
            }
            if (collapse.StartIndex == 0)
            {
                buffer.Append(':');
            }
            var collapseEndIndex = collapse.StartIndex + collapse.Length;
            for (var i = collapseEndIndex; i < segments.Length; i++)
            {
                buffer.AppendFormat(":{0:x}", segments[i]);
            }
            if (collapseEndIndex == segments.Length)
            {
                buffer.Append(':');
            }
            return buffer.ToString();
        }

        private class Subset
        {
            public int Length { get; set; }

            public int StartIndex { get; set; }
        }
    }
}
