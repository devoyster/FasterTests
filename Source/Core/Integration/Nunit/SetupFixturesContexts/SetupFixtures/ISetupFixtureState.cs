using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public interface ISetupFixtureState
    {
        bool IsExecuted { get; }

        bool IsSucceeded { get; }

        bool IsFailed { get; }

        void SetParentFailed(IObserver<TestResult> resultsObserver);

        void Setup(IObserver<TestResult> resultsObserver);

        void Teardown(IObserver<TestResult> resultsObserver);

        ISetupFixtureState NextState();
    }
}