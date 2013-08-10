using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly.Namespace
{
    [SetUpFixture]
    public class SetUpFixture
    {
        public static bool WasInvoked { get; set; }

        [SetUp]
        public void Setup()
        {
            WasInvoked = true;
        }
    }
}