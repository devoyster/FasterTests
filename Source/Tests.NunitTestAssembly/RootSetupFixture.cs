using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly
{
    [SetUpFixture]
    public class RootSetupFixture
    {
        public static bool WasInvoked { get; set; }

        [SetUp]
        public void Setup()
        {
            WasInvoked = true;
        }
    }
}