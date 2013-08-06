using NUnit.Core;

namespace FasterTests.Core.Implementation.Integration.Nunit
{
    public static class NunitInitializer
    {
        public static void EnsureInitialized()
        {
            if (!CoreExtensions.Host.Initialized)
            {
                CoreExtensions.Host.InitializeService();
            }
        }
    }
}