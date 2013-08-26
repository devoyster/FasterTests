using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly
{
    [TestFixture]
    [Ignore]
    public class IgnoredTestFixture
    {
        [Test]
        public void Method()
        {
        }
    }
}