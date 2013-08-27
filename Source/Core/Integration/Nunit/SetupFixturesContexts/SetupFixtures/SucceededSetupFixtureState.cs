using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public class SucceededSetupFixtureState : ISetupFixtureState
    {
        private readonly Lazy<ISetupFixtureAdapter> _lazyAdapter;
        private bool _isTeardownExecuted;

        public SucceededSetupFixtureState(Lazy<ISetupFixtureAdapter> lazyAdapter)
        {
            _lazyAdapter = lazyAdapter;
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

        public void SetParentFailed(IObserver<TestResult> resultsObserver)
        {
            throw new InvalidOperationException("Fixture was already set up");
        }

        public void Setup(IObserver<TestResult> resultsObserver)
        {
            throw new InvalidOperationException("Fixture was already set up");
        }

        public void Teardown(IObserver<TestResult> resultsObserver)
        {
            _lazyAdapter.Value.Teardown();
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