using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.host;
using UriSyntax.port;
using UriSyntax.userinfo;

namespace UriSyntax.authority
{
    public class AuthorityLexerFactory : RuleLexerFactory<Authority>
    {
        static AuthorityLexerFactory()
        {
            Default = new AuthorityLexerFactory(
                userinfo.UserInformationLexerFactory.Default.Singleton(),
                host.HostLexerFactory.Default.Singleton(),
                port.PortLexerFactory.Default.Singleton());
        }

        public AuthorityLexerFactory(
            [NotNull] ILexerFactory<UserInformation> userInformationLexerFactory,
            [NotNull] ILexerFactory<Host> hostLexerFactory,
            [NotNull] ILexerFactory<Port> portLexerFactory)
        {
            if (userInformationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(userInformationLexerFactory));
            }
            if (hostLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hostLexerFactory));
            }
            if (portLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(portLexerFactory));
            }
            UserInformationLexerFactory = userInformationLexerFactory;
            HostLexerFactory = hostLexerFactory;
            PortLexerFactory = portLexerFactory;
        }

        [NotNull]
        public static AuthorityLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Host> HostLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Port> PortLexerFactory { get; }

        [NotNull]
        public ILexerFactory<UserInformation> UserInformationLexerFactory { get; }

        public override ILexer<Authority> Create()
        {
            var at = Terminal.Create(@"@", StringComparer.Ordinal);
            var userinfoseq = Concatenation.Create(UserInformationLexerFactory.Create(), at);
            var optuserinfo = Option.Create(userinfoseq);
            var colon = Terminal.Create(@":", StringComparer.Ordinal);
            var portseq = Concatenation.Create(colon, PortLexerFactory.Create());
            var optport = Option.Create(portseq);
            var innerLexer = Concatenation.Create(optuserinfo, HostLexerFactory.Create(), optport);
            return new AuthorityLexer(innerLexer);
        }
    }
}
