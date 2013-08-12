using System;

namespace FasterTests.Core.Interfaces.Integration
{
    public interface ITestFrameworkInitializer : IDisposable
    {
        void EnsureInitialized();
    }
}