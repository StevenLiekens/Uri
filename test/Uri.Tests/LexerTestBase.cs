using System.Collections.Generic;
using SimpleInjector;
using Txt.ABNF;
using Registration = Txt.Core.Registration;

namespace Uri
{
    public class LexerTestBase
    {
        protected readonly Container Container = new Container();

        public LexerTestBase()
        {
            var registrations = new List<Registration>();
            registrations.AddRange(AbnfRegistrations.GetRegistrations(Container.GetInstance));
            registrations.AddRange(UriRegistrations.GetRegistrations(Container.GetInstance));
            foreach (var registration in registrations)
            {
                if (registration.Implementation != null)
                {
                    Container.RegisterSingleton(registration.Service, registration.Implementation);
                }
                if (registration.Instance != null)
                {
                    Container.RegisterSingleton(registration.Service, registration.Instance);
                }
                if (registration.Factory != null)
                {
                    Container.RegisterSingleton(registration.Service, registration.Factory);
                }
            }
        }
    }
}
