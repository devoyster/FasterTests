using NUnit.Core;

namespace FasterTests.Core.Integration.Nunit
{
    public interface ITestFilterProvider
    {
        ITestFilter TestFilter { get; }
    }
}