using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit
{
    public interface ISetupFixturesContext
    {
        bool SetupFor(TestDescriptor test, IObserver<TestResult> resultsObserver);

        void TeardownAll(IObserver<TestResult> resultsObserver);
    }
}