using System;
using System.Collections.Generic;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Interfaces.Workers
{
    public interface ITestWorkersPool
    {
        IObservable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests);
    }
}