using System.Collections.Generic;
using FasterTests.Core.Models;

namespace FasterTests.Core
{
    public interface ITestDispatcher
    {
        IEnumerable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests);
    }
}