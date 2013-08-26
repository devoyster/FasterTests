using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees
{
    public class RootSetupFixtureStub : ISetupFixture
    {
        public static readonly ISetupFixture Instance = new RootSetupFixtureStub();

        public Type Type
        {
            get { throw new NotSupportedException(); }
        }

        public SetupFixtureState State
        {
            get { return SetupFixtureState.SetupSucceeded; }
        }

        public bool IsRequiredFor(TestDescriptor test)
        {
            return true;
        }

        public bool IsRequiredFor(ISetupFixture otherFixture)
        {
            return true;
        }

        public void SetParentFailed(IObserver<TestResult> resultsObserver)
        {
            throw new NotSupportedException();
        }

        public void Setup(IObserver<TestResult> resultsObserver)
        {
        }

        public void Teardown(IObserver<TestResult> resultsObserver)
        {
        }
    }
}