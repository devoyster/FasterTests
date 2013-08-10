using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly
{
    public class PassingTest
    {
        [Test]
        public void Method()
        {
            Assert.That(true);
        }
    }
}