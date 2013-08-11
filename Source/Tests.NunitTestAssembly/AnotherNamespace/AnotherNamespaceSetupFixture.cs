using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly.AnotherNamespace
{
    [SetUpFixture]
    public class AnotherNamespaceSetupFixture
    {
        public static int InvocationCount { get; set; }

        [SetUp]
        public void Setup()
        {
            InvocationCount++;
        }
    }
}