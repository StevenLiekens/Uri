using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.gen_delims
{
    public class GenericDelimiterLexerFactory : RuleLexerFactory<GenericDelimiter>
    {
        static GenericDelimiterLexerFactory()
        {
            Default = new GenericDelimiterLexerFactory();
        }

        [NotNull]
        public static GenericDelimiterLexerFactory Default { get; }

        public override ILexer<GenericDelimiter> Create()
        {
            // ":" / "/" / "?" / "#" / "[" / "]" / "@"
            var innerLexer = Alternation.Create(
                Terminal.Create(@":", StringComparer.Ordinal),
                Terminal.Create(@"/", StringComparer.Ordinal),
                Terminal.Create(@"?", StringComparer.Ordinal),
                Terminal.Create(@"#", StringComparer.Ordinal),
                Terminal.Create(@"[", StringComparer.Ordinal),
                Terminal.Create(@"]", StringComparer.Ordinal),
                Terminal.Create(@"@", StringComparer.Ordinal));

            // gen-delims
            return new GenericDelimiterLexer(innerLexer);
        }
    }
}
