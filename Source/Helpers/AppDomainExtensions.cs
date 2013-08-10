using System;
using System.Reflection;

namespace FasterTests.Helpers
{
    public static class AppDomainExtensions
    {
        public static T CreateInstanceFromAndUnwrap<T>(this AppDomain domain) where T : MarshalByRefObject
        {
            var type = typeof (T);
            return (T)domain.CreateInstanceFromAndUnwrap(type.Assembly.Location, type.FullName, false, BindingFlags.Default, null, null, null, null);
        }
    }
}