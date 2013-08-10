using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly
{
    public class TestWithFailingAssert
    {
        [Test]
        public void Method()
        {
            Assert.That(false);
        }
    }
}