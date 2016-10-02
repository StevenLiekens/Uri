using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.gen_delims
{
    public class GenericDelimiterLexerFactory : LexerFactory<GenericDelimiter>
    {
        static GenericDelimiterLexerFactory()
        {
            Default = new GenericDelimiterLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default);
        }

        public GenericDelimiterLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
        }

        public static GenericDelimiterLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<GenericDelimiter> Create()
        {
            // ":" / "/" / "?" / "#" / "[" / "]" / "@"
            var innerLexer = AlternationLexerFactory.Create(
                TerminalLexerFactory.Create(@":", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"[", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"]", StringComparer.Ordinal),
                TerminalLexerFactory.Create(@"@", StringComparer.Ordinal));

            // gen-delims
            return new GenericDelimiterLexer(innerLexer);
        }
    }
}
