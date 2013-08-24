using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit
{
    public interface ISetupFixture
    {
        bool IsSetupSucceeded { get; }

        bool ShouldRunFor(TestDescriptor test);

        void Setup(IObserver<TestResult> resultsObserver);

        void Teardown(IObserver<TestResult> resultsObserver);
    }
}