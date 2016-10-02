using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;
using Txt.Core;
using UriSyntax.sub_delims;
using UriSyntax.unreserved;

namespace UriSyntax.IPvFuture
{
    public class IPvFutureLexerFactory : LexerFactory<IPvFuture>
    {
        static IPvFutureLexerFactory()
        {
            Default = new IPvFutureLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                Txt.ABNF.Core.HEXDIG.HexadecimalDigitLexerFactory.Default,
                unreserved.UnreservedLexerFactory.Default,
                sub_delims.SubcomponentsDelimiterLexerFactory.Default);
        }

        public IPvFutureLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory,
            [NotNull] ILexerFactory<Unreserved> unreservedLexerFactory,
            [NotNull] ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
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
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            HexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory.Singleton();
            UnreservedLexerFactory = unreservedLexerFactory.Singleton();
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory.Singleton();
        }

        public static IPvFutureLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<HexadecimalDigit> HexadecimalDigitLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public ILexerFactory<Unreserved> UnreservedLexerFactory { get; }

        public override ILexer<IPvFuture> Create()
        {
            // "v"
            var v = TerminalLexerFactory.Create(@"v", StringComparer.OrdinalIgnoreCase);

            // "."
            var dot = TerminalLexerFactory.Create(@".", StringComparer.Ordinal);

            // ":"
            var colon = TerminalLexerFactory.Create(@":", StringComparer.Ordinal);

            // 1*HEXDIG
            var hexadecimalDigitLexer = HexadecimalDigitLexerFactory.Create();
            var r = RepetitionLexerFactory.Create(hexadecimalDigitLexer, 1, int.MaxValue);

            // unreserved / sub-delims / ":"
            var unreservedLexer = UnreservedLexerFactory.Create();
            var subcomponentsDelimiterLexer = SubcomponentsDelimiterLexerFactory.Create();
            var a = AlternationLexerFactory.Create(unreservedLexer, subcomponentsDelimiterLexer, colon);

            // 1*( unreserved / sub-delims / ":" )
            var s = RepetitionLexerFactory.Create(a, 1, int.MaxValue);

            // "v" 1*HEXDIG "." 1*( unreserved / sub-delims / ":" )
            var innerLexer = ConcatenationLexerFactory.Create(v, r, dot, s);

            // IPvFuture
            return new IPvFutureLexer(innerLexer);
        }
    }
}
