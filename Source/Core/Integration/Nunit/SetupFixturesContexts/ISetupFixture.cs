using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public interface ISetupFixture
    {
        Type Type { get; }

        SetupFixtureState State { get; }

        bool IsRequiredFor(TestDescriptor test);

        void Setup(IObserver<TestResult> resultsObserver);

        void Teardown(IObserver<TestResult> resultsObserver);
    }
}