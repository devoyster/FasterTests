using System;

namespace FasterTests.Tests.Helpers
{
    public class TestCrossDomainObject : MarshalByRefObject
    {
        public string AppDomainName
        {
            get { return AppDomain.CurrentDomain.FriendlyName; }
        }
    }
}