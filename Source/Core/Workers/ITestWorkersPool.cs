using System;
using System.Collections.Generic;
using Funt.Core.Models;

namespace Funt.Core.Workers
{
    public interface ITestWorkersPool
    {
        IObservable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests);
    }
}