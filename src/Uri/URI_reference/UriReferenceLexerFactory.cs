using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.relative_ref;
using UriSyntax.URI;

namespace UriSyntax.URI_reference
{
    public class UriReferenceLexerFactory : ILexerFactory<UriReference>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<RelativeReference> relativeReferenceLexer;

        private readonly ILexer<UniformResourceIdentifier> uriLexer;

        public UriReferenceLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<UniformResourceIdentifier> uriLexer,
            [NotNull] ILexer<RelativeReference> relativeReferenceLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (uriLexer == null)
            {
                throw new ArgumentNullException(nameof(uriLexer));
            }
            if (relativeReferenceLexer == null)
            {
                throw new ArgumentNullException(nameof(relativeReferenceLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.uriLexer = uriLexer;
            this.relativeReferenceLexer = relativeReferenceLexer;
        }

        public ILexer<UriReference> Create()
        {
            var innerLexer = alternationLexerFactory.Create(uriLexer, relativeReferenceLexer);
            return new UriReferenceLexer(innerLexer);
        }
    }
}
