using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.relative_ref;
using UriSyntax.URI;

namespace UriSyntax.URI_reference
{
    public class UriReferenceLexerFactory : RuleLexerFactory<UriReference>
    {
        static UriReferenceLexerFactory()
        {
            Default = new UriReferenceLexerFactory(
                UniformResourceIdentifierLexerFactory.Default.Singleton(),
                relative_ref.RelativeReferenceLexerFactory.Default.Singleton());
        }

        public UriReferenceLexerFactory(
            [NotNull] ILexerFactory<UniformResourceIdentifier> uriLexerFactory,
            [NotNull] ILexerFactory<RelativeReference> relativeReferenceLexerFactory)
        {
            if (uriLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(uriLexerFactory));
            }
            if (relativeReferenceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(relativeReferenceLexerFactory));
            }
            UriLexerFactory = uriLexerFactory;
            RelativeReferenceLexerFactory = relativeReferenceLexerFactory;
        }

        [NotNull]
        public static UriReferenceLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<RelativeReference> RelativeReferenceLexerFactory { get; }

        [NotNull]
        public ILexerFactory<UniformResourceIdentifier> UriLexerFactory { get; }

        public override ILexer<UriReference> Create()
        {
            var innerLexer = Alternation.Create(
                UriLexerFactory.Create(),
                RelativeReferenceLexerFactory.Create());
            return new UriReferenceLexer(innerLexer);
        }
    }
}
