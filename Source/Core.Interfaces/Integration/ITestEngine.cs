using System;
using System.Collections.Generic;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Interfaces.Integration
{
    public interface ITestEngine
    {
        IObservable<TestResult> RunTests(IEnumerable<TestDescriptor> tests);
    }
}