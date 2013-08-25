using System;
using NUnit.Core;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public class SetupFixtureAdapter : SetUpFixture
    {
        public SetupFixtureAdapter(Type type) : base(type)
        {
        }

        public TestResult Setup()
        {
            var testResult = CreateNunitTestResult();

            DoOneTimeSetUp(testResult);
            DoOneTimeBeforeTestSuiteActions(testResult);

            return testResult;
        }

        public TestResult Teardown()
        {
            var testResult = CreateNunitTestResult();

            DoOneTimeAfterTestSuiteActions(testResult);
            DoOneTimeTearDown(testResult);

            return testResult;
        }

        private TestResult CreateNunitTestResult()
        {
            return new TestResult(this);
        }
    }
}