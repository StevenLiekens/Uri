using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.host;
using UriSyntax.port;
using UriSyntax.userinfo;

namespace UriSyntax.authority
{
    public class AuthorityLexerFactory : ILexerFactory<Authority>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Host> hostLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<Port> portLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<UserInformation> userInformationLexer;

        public AuthorityLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<UserInformation> userInformationLexer,
            [NotNull] ILexer<Host> hostLexer,
            [NotNull] ILexer<Port> portLexer)
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
            if (userInformationLexer == null)
            {
                throw new ArgumentNullException(nameof(userInformationLexer));
            }
            if (hostLexer == null)
            {
                throw new ArgumentNullException(nameof(hostLexer));
            }
            if (portLexer == null)
            {
                throw new ArgumentNullException(nameof(portLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.userInformationLexer = userInformationLexer;
            this.hostLexer = hostLexer;
            this.portLexer = portLexer;
        }

        public ILexer<Authority> Create()
        {
            var at = terminalLexerFactory.Create(@"@", StringComparer.Ordinal);
            var userinfoseq = concatenationLexerFactory.Create(userInformationLexer, at);
            var optuserinfo = optionLexerFactory.Create(userinfoseq);
            var colon = terminalLexerFactory.Create(@":", StringComparer.Ordinal);
            var portseq = concatenationLexerFactory.Create(colon, portLexer);
            var optport = optionLexerFactory.Create(portseq);
            var innerLexer = concatenationLexerFactory.Create(optuserinfo, hostLexer, optport);
            return new AuthorityLexer(innerLexer);
        }
    }
}
