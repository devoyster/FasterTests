using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixtures
{
    public class SetupFixture : ISetupFixture
    {
        private readonly Type _type;
        private readonly SetupFixtureAdapter _adapter;

        public SetupFixture(Type type)
        {
            _type = type;
            _adapter = new SetupFixtureAdapter(type);
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

            var testResult = _adapter.Setup();

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
                    _adapter.Teardown();
                    break;
            }

            State = SetupFixtureState.NoSetupExecuted;
        }
    }
}