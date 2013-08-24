using FasterTests.Core.Interfaces.Integration;
using NUnit.Core;

namespace FasterTests.Core.Integration.Nunit
{
    public class TestFrameworkInitializer : ITestFrameworkInitializer
    {
        public void EnsureInitialized()
        {
            if (!CoreExtensions.Host.Initialized)
            {
                CoreExtensions.Host.InitializeService();

                TestExecutionContext.CurrentContext.TestPackage = new TestPackage("");
            }
        }

        public void Dispose()
        {
        }
    }
}