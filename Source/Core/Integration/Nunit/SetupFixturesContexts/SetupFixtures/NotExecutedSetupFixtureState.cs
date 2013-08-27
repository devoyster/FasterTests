using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public class NotExecutedSetupFixtureState : ISetupFixtureState
    {
        private readonly Lazy<ISetupFixtureAdapter> _lazyAdapter;
        private bool _isSetupSucceeded;
        private bool _isSetupFailed;

        public NotExecutedSetupFixtureState(Lazy<ISetupFixtureAdapter> lazyAdapter)
        {
            _lazyAdapter = lazyAdapter;
        }

        public bool IsExecuted
        {
            get { return false; }
        }

        public bool IsSucceeded
        {
            get { return false; }
        }

        public bool IsFailed
        {
            get { return false; }
        }

        public void SetParentFailed(IObserver<TestResult> resultsObserver)
        {
            _isSetupFailed = true;
        }

        public void Setup(IObserver<TestResult> resultsObserver)
        {
            if (_lazyAdapter.Value.Setup())
            {
                _isSetupSucceeded = true;
            }
            else
            {
                _isSetupFailed = true;
            }
        }

        public void Teardown(IObserver<TestResult> resultsObserver)
        {
            throw new InvalidOperationException("Fixture was not set up");
        }

        public ISetupFixtureState NextState()
        {
            if (_isSetupSucceeded)
            {
                return new SucceededSetupFixtureState(_lazyAdapter);
            }
            if (_isSetupFailed)
            {
                return new FailedSetupFixtureState(_lazyAdapter);
            }

            return this;
        }
    }
}