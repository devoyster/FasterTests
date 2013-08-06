using System;
using System.Collections.Generic;
using FasterTests.Core.Models;

namespace FasterTests.Core.Workers
{
    public interface ITestWorkersPool
    {
        IObservable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests);
    }
}