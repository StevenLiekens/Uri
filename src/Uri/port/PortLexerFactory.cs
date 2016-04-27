using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;

namespace Uri.port
{
    public class PortLexerFactory : ILexerFactory<Port>
    {
        private readonly ILexer<Digit> digitLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public PortLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Digit> digitLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (digitLexer == null)
            {
                throw new ArgumentNullException(nameof(digitLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.digitLexer = digitLexer;
        }

        public ILexer<Port> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(digitLexer, 0, int.MaxValue);
            return new PortLexer(innerLexer);
        }
    }
}
