using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixtures
{
    public class SetupFixture : ISetupFixture
    {
        private readonly Type _type;
        private readonly Lazy<SetupFixtureAdapter> _lazyAdapter;

        public SetupFixture(Type type)
        {
            _type = type;
            _lazyAdapter = new Lazy<SetupFixtureAdapter>(() => new SetupFixtureAdapter(type));
        }

        public Type Type
        {
            get { return _type; }
        }

        public SetupFixtureState State { get; private set; }

        public bool IsRequiredFor(TestDescriptor test)
        {
            return string.IsNullOrEmpty(_type.Namespace) || test.Name.StartsWith(_type.Namespace + ".");
        }

        public void Setup(IObserver<TestResult> resultsObserver)
        {
            if (State != SetupFixtureState.NoSetupExecuted)
            {
                throw new InvalidOperationException("Fixture was already set up");
            }

            var testResult = _lazyAdapter.Value.Setup();

            State = testResult.IsFailure || testResult.IsError
                        ? SetupFixtureState.SetupFailed
                        : SetupFixtureState.SetupSucceeded;
        }

        public void Teardown(IObserver<TestResult> resultsObserver)
        {
            switch (State)
            {
                case SetupFixtureState.NoSetupExecuted:
                    throw new InvalidOperationException("Fixture was not set up");

                case SetupFixtureState.SetupSucceeded:
                    _lazyAdapter.Value.Teardown();
                    break;
            }

            State = SetupFixtureState.NoSetupExecuted;
        }
    }
}