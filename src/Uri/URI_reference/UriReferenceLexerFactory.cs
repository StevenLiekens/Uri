using System;
using Txt;
using Txt.ABNF;
using Uri.relative_ref;
using Uri.URI;

namespace Uri.URI_reference
{
    public class UriReferenceLexerFactory : ILexerFactory<UriReference>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexerFactory<RelativeReference> relativeReferenceLexerFactory;

        private readonly ILexerFactory<UniformResourceIdentifier> uriLexerFactory;

        public UriReferenceLexerFactory(
            IAlternationLexerFactory alternationLexerFactory,
            ILexerFactory<UniformResourceIdentifier> uriLexerFactory,
            ILexerFactory<RelativeReference> relativeReferenceLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }

            if (uriLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(uriLexerFactory));
            }

            if (relativeReferenceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(relativeReferenceLexerFactory));
            }

            this.alternationLexerFactory = alternationLexerFactory;
            this.uriLexerFactory = uriLexerFactory;
            this.relativeReferenceLexerFactory = relativeReferenceLexerFactory;
        }

        public ILexer<UriReference> Create()
        {
            var uri = uriLexerFactory.Create();
            var relativeRef = relativeReferenceLexerFactory.Create();
            var innerLexer = alternationLexerFactory.Create(uri, relativeRef);
            return new UriReferenceLexer(innerLexer);
        }
    }
}