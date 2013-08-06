using System;
using System.Collections.Generic;
using FasterTests.Core.Models;

namespace FasterTests.Core.Integration
{
    public interface ITestEngine
    {
        IObservable<TestResult> RunTests(IEnumerable<TestDescriptor> tests);
    }
}