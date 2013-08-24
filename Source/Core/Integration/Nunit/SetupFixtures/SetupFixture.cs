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

        public bool IsSetupSucceeded { get; private set; }

        public bool IsRequiredFor(TestDescriptor test)
        {
            return string.IsNullOrEmpty(_type.Namespace) || test.Name.StartsWith(_type.Namespace + ".");
        }

        public void Setup(IObserver<Interfaces.Models.TestResult> resultsObserver)
        {
            var testResult = CreateNunitTestResult();

            DoOneTimeSetUp(testResult);
            DoOneTimeBeforeTestSuiteActions(testResult);

            IsSetupSucceeded = !testResult.IsFailure && !testResult.IsError;
        }

        public void Teardown(IObserver<Interfaces.Models.TestResult> resultsObserver)
        {
            if (!IsSetupSucceeded)
            {
                return;
            }

            var testResult = CreateNunitTestResult();

            DoOneTimeAfterTestSuiteActions(testResult);
            DoOneTimeTearDown(testResult);
        }

        private NUnit.Core.TestResult CreateNunitTestResult()
        {
            return new NUnit.Core.TestResult(this);
        }
    }
}