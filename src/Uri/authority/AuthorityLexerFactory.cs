using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.host;
using UriSyntax.port;
using UriSyntax.userinfo;

namespace UriSyntax.authority
{
    public class AuthorityLexerFactory : LexerFactory<Authority>
    {
        static AuthorityLexerFactory()
        {
            Default = new AuthorityLexerFactory(
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.OptionLexerFactory.Default,
                userinfo.UserInformationLexerFactory.Default.Singleton(),
                host.HostLexerFactory.Default.Singleton(),
                port.PortLexerFactory.Default.Singleton());
        }

        public AuthorityLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexerFactory<UserInformation> userInformationLexerFactory,
            [NotNull] ILexerFactory<Host> hostLexerFactory,
            [NotNull] ILexerFactory<Port> portLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
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
            TerminalLexerFactory = terminalLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            OptionLexerFactory = optionLexerFactory;
            UserInformationLexerFactory = userInformationLexerFactory;
            HostLexerFactory = hostLexerFactory;
            PortLexerFactory = portLexerFactory;
        }

        public static AuthorityLexerFactory Default { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<Host> HostLexerFactory { get; }

        public IOptionLexerFactory OptionLexerFactory { get; }

        public ILexerFactory<Port> PortLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public ILexerFactory<UserInformation> UserInformationLexerFactory { get; }

        public override ILexer<Authority> Create()
        {
            var at = TerminalLexerFactory.Create(@"@", StringComparer.Ordinal);
            var userinfoseq = ConcatenationLexerFactory.Create(UserInformationLexerFactory.Create(), at);
            var optuserinfo = OptionLexerFactory.Create(userinfoseq);
            var colon = TerminalLexerFactory.Create(@":", StringComparer.Ordinal);
            var portseq = ConcatenationLexerFactory.Create(colon, PortLexerFactory.Create());
            var optport = OptionLexerFactory.Create(portseq);
            var innerLexer = ConcatenationLexerFactory.Create(optuserinfo, HostLexerFactory.Create(), optport);
            return new AuthorityLexer(innerLexer);
        }
    }
}
