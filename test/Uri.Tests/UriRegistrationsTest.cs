using System.Linq;
using SimpleInjector;
using Txt.ABNF;
using Xunit;

namespace Uri
{
    public class UriRegistrationsTest
    {
        private readonly Container container = new Container();

        [Fact]
        public void GetRegistrations()
        {
            foreach (
                var registration in
                    AbnfRegistrations.GetRegistrations(container.GetInstance)
                                     .Concat(UriRegistrations.GetRegistrations(container.GetInstance)))
            {
                if (registration.Implementation != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Implementation);
                }
                if (registration.Instance != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Instance);
                }
                if (registration.Factory != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Factory);
                }
            }
            container.Verify();
        }
    }
}
