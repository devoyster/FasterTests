using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly
{
    public class IgnoredTest
    {
        [Test]
        [Ignore]
        public void Method()
        {
        }
    }
}