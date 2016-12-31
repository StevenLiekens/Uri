using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;
using Txt.Core;
using UriSyntax.sub_delims;
using UriSyntax.unreserved;

namespace UriSyntax.IPvFuture
{
    // ReSharper disable once InconsistentNaming
    public class IPvFutureLexerFactory : RuleLexerFactory<IPvFuture>
    {
        static IPvFutureLexerFactory()
        {
            Default = new IPvFutureLexerFactory(
                Txt.ABNF.Core.HEXDIG.HexadecimalDigitLexerFactory.Default.Singleton(),
                unreserved.UnreservedLexerFactory.Default.Singleton(),
                sub_delims.SubcomponentsDelimiterLexerFactory.Default.Singleton());
        }

        public IPvFutureLexerFactory(
            [NotNull] ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory,
            [NotNull] ILexerFactory<Unreserved> unreservedLexerFactory,
            [NotNull] ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalDigitLexerFactory));
            }
            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(unreservedLexerFactory));
            }
            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }
            HexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
            UnreservedLexerFactory = unreservedLexerFactory;
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
        }

        [NotNull]
        public static IPvFutureLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HexadecimalDigit> HexadecimalDigitLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Unreserved> UnreservedLexerFactory { get; }

        public override ILexer<IPvFuture> Create()
        {
            // "v"
            var v = Terminal.Create(@"v", StringComparer.OrdinalIgnoreCase);

            // "."
            var dot = Terminal.Create(@".", StringComparer.Ordinal);

            // ":"
            var colon = Terminal.Create(@":", StringComparer.Ordinal);

            // 1*HEXDIG
            var hexadecimalDigitLexer = HexadecimalDigitLexerFactory.Create();
            var r = Repetition.Create(hexadecimalDigitLexer, 1, int.MaxValue);

            // unreserved / sub-delims / ":"
            var unreservedLexer = UnreservedLexerFactory.Create();
            var subcomponentsDelimiterLexer = SubcomponentsDelimiterLexerFactory.Create();
            var a = Alternation.Create(unreservedLexer, subcomponentsDelimiterLexer, colon);

            // 1*( unreserved / sub-delims / ":" )
            var s = Repetition.Create(a, 1, int.MaxValue);

            // "v" 1*HEXDIG "." 1*( unreserved / sub-delims / ":" )
            var innerLexer = Concatenation.Create(v, r, dot, s);

            // IPvFuture
            return new IPvFutureLexer(innerLexer);
        }
    }
}
