using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace UriSyntax.port
{
    public class PortLexerFactory : LexerFactory<Port>
    {
        static PortLexerFactory()
        {
            Default = new PortLexerFactory(
                Txt.ABNF.RepetitionLexerFactory.Default,
                Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public PortLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            RepetitionLexerFactory = repetitionLexerFactory;
            DigitLexerFactory = digitLexerFactory;
        }

        public static PortLexerFactory Default { get; }

        public ILexerFactory<Digit> DigitLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public override ILexer<Port> Create()
        {
            var innerLexer = RepetitionLexerFactory.Create(DigitLexerFactory.Create(), 0, int.MaxValue);
            return new PortLexer(innerLexer);
        }
    }
}
