using System;
using FasterTests.Core.Interfaces.Models;
using NUnit.Core;

namespace FasterTests.Core.Integration.Nunit.SetupFixtures
{
    public class SetupFixture : SetUpFixture, ISetupFixture
    {
        private readonly Type _type;

        public SetupFixture(Type type) : base(type)
        {
            _type = type;
        }

        public SetupFixtureState State { get; private set; }

        public bool IsRequiredFor(TestDescriptor test)
        {
            return string.IsNullOrEmpty(_type.Namespace) || test.Name.StartsWith(_type.Namespace + ".");
        }

        public void Setup(IObserver<Interfaces.Models.TestResult> resultsObserver)
        {
            if (State != SetupFixtureState.NoSetupExecuted)
            {
                throw new InvalidOperationException("Fixture was already set up");
            }

            var testResult = CreateNunitTestResult();
            DoOneTimeSetUp(testResult);
            DoOneTimeBeforeTestSuiteActions(testResult);

            State = testResult.IsFailure || testResult.IsError
                        ? SetupFixtureState.SetupFailed
                        : SetupFixtureState.SetupSucceeded;
        }

        public void Teardown(IObserver<Interfaces.Models.TestResult> resultsObserver)
        {
            switch (State)
            {
                case SetupFixtureState.NoSetupExecuted:
                    throw new InvalidOperationException("Fixture was not set up");

                case SetupFixtureState.SetupSucceeded:
                    var testResult = CreateNunitTestResult();
                    DoOneTimeAfterTestSuiteActions(testResult);
                    DoOneTimeTearDown(testResult);
                    break;
            }

            State = SetupFixtureState.NoSetupExecuted;
        }

        private NUnit.Core.TestResult CreateNunitTestResult()
        {
            return new NUnit.Core.TestResult(this);
        }
    }
}