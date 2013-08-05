using System.Collections.Generic;
using Funt.Core.Models;

namespace Funt.Core
{
    public interface ITestDispatcher
    {
        IEnumerable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests);
    }
}