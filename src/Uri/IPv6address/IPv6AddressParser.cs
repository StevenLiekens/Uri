using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.h16;
using UriSyntax.ls32;

namespace UriSyntax.IPv6address
{
    // ReSharper disable once InconsistentNaming
    public class IPv6AddressParser : Parser<IPv6Address, byte[]>
    {
        private readonly IParser<HexadecimalInt16, byte[]> hexadecimalInt16Parser;

        private readonly IParser<LeastSignificantInt32, byte[]> leastSignificantInt32Parser;

        public IPv6AddressParser(
            [NotNull] IParser<HexadecimalInt16, byte[]> hexadecimalInt16Parser,
            [NotNull] IParser<LeastSignificantInt32, byte[]> leastSignificantInt32Parser)
        {
            if (hexadecimalInt16Parser == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16Parser));
            }
            if (leastSignificantInt32Parser == null)
            {
                throw new ArgumentNullException(nameof(leastSignificantInt32Parser));
            }
            this.hexadecimalInt16Parser = hexadecimalInt16Parser;
            this.leastSignificantInt32Parser = leastSignificantInt32Parser;
        }

        private delegate byte[] BytesFactory();

        protected override byte[] ParseImpl(IPv6Address value)
        {
            var concatenation = (Concatenation)value.Element;
            switch (value.Ordinal)
            {
                case 1:
                    return GetBytes1(concatenation);
                case 2:
                    return GetBytes2(concatenation);
                case 3:
                    return GetBytes3(concatenation);
                case 4:
                    return GetBytes4(concatenation);
                case 5:
                    return GetBytes5(concatenation);
                case 6:
                    return GetBytes6(concatenation);
                case 7:
                    return GetBytes7(concatenation);
                case 8:
                    return GetBytes8(concatenation);
                case 9:
                    return GetBytes9(concatenation);
            }
            return null;
        }

        private byte[] GetBytes1(Concatenation concatenation)
        {
            var ctx = new BytesFactoryContext();
            var rep = (Repetition)concatenation[0];
            for (var i = 0; i < 6; i++)
            {
                var seq1 = (Concatenation)rep[i];
                var h16 = (HexadecimalInt16)seq1[0];
                ctx.RightAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var ls32 = (LeastSignificantInt32)concatenation[1];
            ctx.RightAlign.Add(() => leastSignificantInt32Parser.Parse(ls32));
            return ctx.GetResult();
        }

        private byte[] GetBytes2(Concatenation concatenation)
        {
            var ctx = new BytesFactoryContext();
            var rep = (Repetition)concatenation[1];
            for (var i = 0; i < 5; i++)
            {
                var seq1 = (Concatenation)rep[i];
                var h16 = (HexadecimalInt16)seq1[0];
                ctx.RightAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var ls32 = (LeastSignificantInt32)concatenation[2];
            ctx.RightAlign.Add(() => leastSignificantInt32Parser.Parse(ls32));
            return ctx.GetResult();
        }

        private byte[] GetBytes3(Concatenation concatenation)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)concatenation[0];
            if (opt1.Count != 0)
            {
                var h16 = (HexadecimalInt16)opt1[0];
                ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var rep = (Repetition)concatenation[2];
            for (var i = 0; i < 4; i++)
            {
                var seq1 = (Concatenation)rep[i];
                var h16 = (HexadecimalInt16)seq1[0];
                ctx.RightAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var ls32 = (LeastSignificantInt32)concatenation[3];
            ctx.RightAlign.Add(() => leastSignificantInt32Parser.Parse(ls32));
            return ctx.GetResult();
        }

        private byte[] GetBytes4(Concatenation concatenation)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)concatenation[0];
            if (opt1.Count != 0)
            {
                GetBytesh16Alt2((Alternation)opt1[0], ctx);
            }
            var rep = (Repetition)concatenation[2];
            for (var i = 0; i < 3; i++)
            {
                var seq1 = (Concatenation)rep[i];
                var h16 = (HexadecimalInt16)seq1[0];
                ctx.RightAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var ls32 = (LeastSignificantInt32)concatenation[3];
            ctx.RightAlign.Add(() => leastSignificantInt32Parser.Parse(ls32));
            return ctx.GetResult();
        }

        private byte[] GetBytes5(Concatenation concatenation)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)concatenation[0];
            if (opt1.Count != 0)
            {
                GetBytesh16Alt3((Alternation)opt1[0], ctx);
            }
            var rep = (Repetition)concatenation[2];
            for (var i = 0; i < 2; i++)
            {
                var seq1 = (Concatenation)rep[i];
                var h16 = (HexadecimalInt16)seq1[0];
                ctx.RightAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var ls32 = (LeastSignificantInt32)concatenation[3];
            ctx.RightAlign.Add(() => leastSignificantInt32Parser.Parse(ls32));
            return ctx.GetResult();
        }

        private byte[] GetBytes6(Concatenation concatenation)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)concatenation[0];
            if (opt1.Count != 0)
            {
                GetBytesh16Alt4((Alternation)opt1[0], ctx);
            }
            var h16 = (HexadecimalInt16)concatenation[2];
            ctx.RightAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            var ls32 = (LeastSignificantInt32)concatenation[4];
            ctx.RightAlign.Add(() => leastSignificantInt32Parser.Parse(ls32));
            return ctx.GetResult();
        }

        private byte[] GetBytes7(Concatenation concatenation)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)concatenation[0];
            if (opt1.Count != 0)
            {
                GetBytesh16Alt5((Alternation)opt1[0], ctx);
            }
            var ls32 = (LeastSignificantInt32)concatenation[2];
            ctx.RightAlign.Add(() => leastSignificantInt32Parser.Parse(ls32));
            return ctx.GetResult();
        }

        private byte[] GetBytes8(Concatenation concatenation)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)concatenation[0];
            if (opt1.Count != 0)
            {
                GetBytesh16Alt6((Alternation)opt1[0], ctx);
            }
            var h16 = (HexadecimalInt16)concatenation[2];
            ctx.RightAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            return ctx.GetResult();
        }

        private byte[] GetBytes9(Concatenation concatenation)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)concatenation[0];
            if (opt1.Count != 0)
            {
                GetBytesh16Alt7((Alternation)opt1[0], ctx);
            }
            return ctx.GetResult();
        }

        private void GetBytesh16Alt2(Alternation alternation, BytesFactoryContext ctx)
        {
            if (alternation.Ordinal == 2)
            {
                ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse((HexadecimalInt16)alternation.Element));
                return;
            }
            var seq = (Concatenation)alternation.Element;
            ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse((HexadecimalInt16)seq[0]));
            ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse((HexadecimalInt16)seq[2]));
        }

        private void GetBytesh16Alt3(Alternation alternation, BytesFactoryContext ctx)
        {
            if (alternation.Ordinal == 2)
            {
                GetBytesh16Alt2((Alternation)alternation.Element, ctx);
                return;
            }
            var concatenation = (Concatenation)alternation.Element;
            var rep = (Repetition)concatenation[0];
            for (var i = 0; i < 2; i++)
            {
                var seq1 = (Concatenation)rep[i];
                var h16 = (HexadecimalInt16)seq1[0];
                ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var trailer = (HexadecimalInt16)concatenation[1];
            ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(trailer));
        }

        private void GetBytesh16Alt4(Alternation alternation, BytesFactoryContext ctx)
        {
            if (alternation.Ordinal == 2)
            {
                GetBytesh16Alt3((Alternation)alternation.Element, ctx);
                return;
            }
            var concatenation = (Concatenation)alternation.Element;
            var rep = (Repetition)concatenation[0];
            for (var i = 0; i < 3; i++)
            {
                var seq1 = (Concatenation)rep[i];
                var h16 = (HexadecimalInt16)seq1[0];
                ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var trailer = (HexadecimalInt16)concatenation[1];
            ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(trailer));
        }

        private void GetBytesh16Alt5(Alternation alternation, BytesFactoryContext ctx)
        {
            if (alternation.Ordinal == 2)
            {
                GetBytesh16Alt4((Alternation)alternation.Element, ctx);
                return;
            }
            var concatenation = (Concatenation)alternation.Element;
            var rep = (Repetition)concatenation[0];
            for (var i = 0; i < 4; i++)
            {
                var seq1 = (Concatenation)rep[i];
                var h16 = (HexadecimalInt16)seq1[0];
                ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var trailer = (HexadecimalInt16)concatenation[1];
            ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(trailer));
        }

        private void GetBytesh16Alt6(Alternation alternation, BytesFactoryContext ctx)
        {
            if (alternation.Ordinal == 2)
            {
                GetBytesh16Alt5((Alternation)alternation.Element, ctx);
                return;
            }
            var concatenation = (Concatenation)alternation.Element;
            var rep = (Repetition)concatenation[0];
            for (var i = 0; i < 5; i++)
            {
                var seq1 = (Concatenation)rep[i];
                var h16 = (HexadecimalInt16)seq1[0];
                ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var trailer = (HexadecimalInt16)concatenation[1];
            ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(trailer));
        }

        private void GetBytesh16Alt7(Alternation alternation, BytesFactoryContext ctx)
        {
            if (alternation.Ordinal == 2)
            {
                GetBytesh16Alt6((Alternation)alternation.Element, ctx);
                return;
            }
            var concatenation = (Concatenation)alternation.Element;
            var rep = (Repetition)concatenation[0];
            for (var i = 0; i < 6; i++)
            {
                var seq1 = (Concatenation)rep[i];
                var h16 = (HexadecimalInt16)seq1[0];
                ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(h16));
            }
            var trailer = (HexadecimalInt16)concatenation[1];
            ctx.LeftAlign.Add(() => hexadecimalInt16Parser.Parse(trailer));
        }

        private class BytesFactoryContext
        {
            public IList<BytesFactory> LeftAlign { get; } = new List<BytesFactory>();

            public IList<BytesFactory> RightAlign { get; } = new List<BytesFactory>();

            public byte[] GetResult()
            {
                var result = new byte[16];
                if (LeftAlign.Count != 0)
                {
                    var l = LeftAlign.SelectMany(factory => factory()).ToArray();
                    l.CopyTo(result, 0);
                }
                if (RightAlign.Count != 0)
                {
                    var r = RightAlign.SelectMany(factory => factory()).ToArray();
                    r.CopyTo(result, 16 - r.Length);
                }
                return result;
            }
        }
    }
}
