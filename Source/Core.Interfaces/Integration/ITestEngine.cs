using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Interfaces.Integration
{
    public interface ITestEngine : IDisposable
    {
        IObservable<TestResult> Results { get; }

        void RunTest(TestDescriptor test);
    }
}