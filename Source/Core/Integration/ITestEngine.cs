using System;
using System.Collections.Generic;
using Funt.Core.Models;

namespace Funt.Core.Integration
{
    public interface ITestEngine
    {
        IObservable<TestResult> RunTests(IEnumerable<TestDescriptor> tests);
    }
}