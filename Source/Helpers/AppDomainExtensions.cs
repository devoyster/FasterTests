using System;

namespace Funt.Helpers
{
    public static class AppDomainExtensions
    {
        public static T CreateInstanceAndUnwrap<T>(this AppDomain domain) where T : MarshalByRefObject
        {
            var type = typeof (T);
            return (T)domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
        }
    }
}