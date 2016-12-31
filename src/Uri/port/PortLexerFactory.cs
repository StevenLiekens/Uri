using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace UriSyntax.port
{
    public class PortLexerFactory : RuleLexerFactory<Port>
    {
        static PortLexerFactory()
        {
            Default = new PortLexerFactory(Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public PortLexerFactory(
            [NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            DigitLexerFactory = digitLexerFactory;
        }

        [NotNull]
        public static PortLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; }

        public override ILexer<Port> Create()
        {
            var innerLexer = Repetition.Create(DigitLexerFactory.Create(), 0, int.MaxValue);
            return new PortLexer(innerLexer);
        }
    }
}
