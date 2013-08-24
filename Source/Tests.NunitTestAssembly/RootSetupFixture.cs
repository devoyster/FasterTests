using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly
{
    [SetUpFixture]
    public class RootSetupFixture
    {
        public static bool SetupWasInvoked { get; set; }

        public static bool TeardownWasInvoked { get; set; }

        [SetUp]
        public void Setup()
        {
            SetupWasInvoked = true;
        }

        [TearDown]
        public void Teardown()
        {
            TeardownWasInvoked = true;
        }
    }
}