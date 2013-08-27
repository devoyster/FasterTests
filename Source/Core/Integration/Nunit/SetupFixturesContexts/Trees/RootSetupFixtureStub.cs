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

        public bool IsExecuted
        {
            get { return true; }
        }

        public bool IsSucceeded
        {
            get { return true; }
        }

        public bool IsFailed
        {
            get { return false; }
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