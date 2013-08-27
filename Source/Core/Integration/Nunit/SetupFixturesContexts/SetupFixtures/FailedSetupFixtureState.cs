using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public class FailedSetupFixtureState : ISetupFixtureState
    {
        private readonly Lazy<ISetupFixtureAdapter> _lazyAdapter;
        private bool _isTeardownExecuted;

        public FailedSetupFixtureState(Lazy<ISetupFixtureAdapter> lazyAdapter)
        {
            _lazyAdapter = lazyAdapter;
        }

        public bool IsExecuted
        {
            get { return true; }
        }

        public bool IsSucceeded
        {
            get { return false; }
        }

        public bool IsFailed
        {
            get { return true; }
        }

        public void SetParentFailed(IObserver<TestResult> resultsObserver)
        {
            throw new InvalidOperationException("Fixture has already failed");
        }

        public void Setup(IObserver<TestResult> resultsObserver)
        {
            throw new InvalidOperationException("Fixture has already failed");
        }

        public void Teardown(IObserver<TestResult> resultsObserver)
        {
            _isTeardownExecuted = true;
        }

        public ISetupFixtureState NextState()
        {
            if (_isTeardownExecuted)
            {
                return new NotExecutedSetupFixtureState(_lazyAdapter);
            }

            return this;
        }
    }
}