using System.Collections.Generic;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Interfaces
{
    public interface ITestDispatcher
    {
        IEnumerable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests);
    }
}