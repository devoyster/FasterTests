using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly
{
    public class ExplicitTest
    {
        [Test]
        [Explicit]
        public void Method()
        {
        }
    }
}