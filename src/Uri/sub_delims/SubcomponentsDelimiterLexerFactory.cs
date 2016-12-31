using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace UriSyntax.sub_delims
{
    public class SubcomponentsDelimiterLexerFactory : RuleLexerFactory<SubcomponentsDelimiter>
    {
        static SubcomponentsDelimiterLexerFactory()
        {
            Default = new SubcomponentsDelimiterLexerFactory();
        }

        [NotNull]
        public static SubcomponentsDelimiterLexerFactory Default { get; }

        public override ILexer<SubcomponentsDelimiter> Create()
        {
            // "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
            var innerLexer = Alternation.Create(
                Terminal.Create(@"!", StringComparer.Ordinal),
                Terminal.Create(@"$", StringComparer.Ordinal),
                Terminal.Create(@"&", StringComparer.Ordinal),
                Terminal.Create(@"'", StringComparer.Ordinal),
                Terminal.Create(@"(", StringComparer.Ordinal),
                Terminal.Create(@")", StringComparer.Ordinal),
                Terminal.Create(@"*", StringComparer.Ordinal),
                Terminal.Create(@"+", StringComparer.Ordinal),
                Terminal.Create(@",", StringComparer.Ordinal),
                Terminal.Create(@";", StringComparer.Ordinal),
                Terminal.Create(@"=", StringComparer.Ordinal));

            // sub-delims
            return new SubcomponentsDelimiterLexer(innerLexer);
        }
    }
}
