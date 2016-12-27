using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.relative_ref;
using UriSyntax.URI;

namespace UriSyntax.URI_reference
{
    public class UriReferenceLexerFactory : LexerFactory<UriReference>
    {
        static UriReferenceLexerFactory()
        {
            Default = new UriReferenceLexerFactory(
                Txt.ABNF.AlternationLexerFactory.Default,
                UniformResourceIdentifierLexerFactory.Default.Singleton(),
                relative_ref.RelativeReferenceLexerFactory.Default.Singleton());
        }

        public UriReferenceLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexerFactory<UniformResourceIdentifier> uriLexerFactory,
            [NotNull] ILexerFactory<RelativeReference> relativeReferenceLexerFactory)
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
            AlternationLexerFactory = alternationLexerFactory;
            UriLexerFactory = uriLexerFactory;
            RelativeReferenceLexerFactory = relativeReferenceLexerFactory;
        }

        [NotNull]
        public static UriReferenceLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<RelativeReference> RelativeReferenceLexerFactory { get; }

        [NotNull]
        public ILexerFactory<UniformResourceIdentifier> UriLexerFactory { get; }

        public override ILexer<UriReference> Create()
        {
            var innerLexer = AlternationLexerFactory.Create(
                UriLexerFactory.Create(),
                RelativeReferenceLexerFactory.Create());
            return new UriReferenceLexer(innerLexer);
        }
    }
}
