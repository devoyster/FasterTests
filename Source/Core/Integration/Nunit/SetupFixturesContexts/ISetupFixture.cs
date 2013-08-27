using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public interface ISetupFixture
    {
        Type Type { get; }

        bool IsExecuted { get; }

        bool IsSucceeded { get; }

        bool IsFailed { get; }

        bool IsRequiredFor(TestDescriptor test);

        bool IsRequiredFor(ISetupFixture otherFixture);

        void SetParentFailed(IObserver<TestResult> resultsObserver);

        void Setup(IObserver<TestResult> resultsObserver);

        void Teardown(IObserver<TestResult> resultsObserver);
    }
}