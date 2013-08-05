using NUnit.Core;

namespace Funt.Core.Integration.Implementation.Nunit
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