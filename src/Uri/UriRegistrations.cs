using System;
using System.Collections.Generic;
using System.Reflection;
using Txt;

namespace Uri
{
    public class UriRegistrations : Registrations
    {
        public static IEnumerable<Registration> GetRegistrations(GetInstanceDelegate getInstance)
        {
            if (getInstance == null)
            {
                throw new ArgumentNullException(nameof(getInstance));
            }
            return GetRegistrations(typeof(UriRegistrations).GetTypeInfo().Assembly, getInstance);
        }
    }
}
