using System;
using NUnit.Core;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters
{
    public class SetupFixtureAdapter : SetUpFixture, ISetupFixtureAdapter
    {
        public SetupFixtureAdapter(Type type) : base(type)
        {
        }

        public bool Setup()
        {
            var testResult = CreateNunitTestResult();

            DoOneTimeSetUp(testResult);
            DoOneTimeBeforeTestSuiteActions(testResult);

            return !testResult.IsFailure && !testResult.IsError;
        }

        public void Teardown()
        {
            var testResult = CreateNunitTestResult();

            DoOneTimeAfterTestSuiteActions(testResult);
            DoOneTimeTearDown(testResult);
        }

        private TestResult CreateNunitTestResult()
        {
            return new TestResult(this);
        }
    }
}