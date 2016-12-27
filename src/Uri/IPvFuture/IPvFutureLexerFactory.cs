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
                Txt.ABNF.Core.HEXDIG.HexadecimalDigitLexerFactory.Default.Singleton(),
                unreserved.UnreservedLexerFactory.Default.Singleton(),
                sub_delims.SubcomponentsDelimiterLexerFactory.Default.Singleton());
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
            HexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
            UnreservedLexerFactory = unreservedLexerFactory;
            SubcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
        }

        [NotNull]
        public static IPvFutureLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<HexadecimalDigit> HexadecimalDigitLexerFactory { get; }

        [NotNull]
        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        [NotNull]
        public ILexerFactory<SubcomponentsDelimiter> SubcomponentsDelimiterLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        [NotNull]
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
