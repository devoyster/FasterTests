using System;
using NUnit.Core;
using TestResult = FasterTests.Core.Interfaces.Models.TestResult;

namespace FasterTests.Core.Implementation.Integration.Nunit
{
    public class NunitObserverEventListener : EventListener
    {
        private readonly IObserver<TestResult> _observer;

        public NunitObserverEventListener(IObserver<TestResult> observer)
        {
            _observer = observer;
        }

        public void RunStarted(string name, int testCount)
        {
        }

        public void RunFinished(NUnit.Core.TestResult result)
        {
        }

        public void RunFinished(Exception exception)
        {
        }

        public void TestStarted(TestName testName)
        {
        }

        public void TestFinished(NUnit.Core.TestResult result)
        {
            _observer.OnNext(new TestResult
                                 {
                                     IsSuccess = result.IsSuccess,
                                     IsIgnored = result.ResultState == ResultState.Ignored || result.ResultState == ResultState.Skipped,
                                     ErrorMessage = result.Message + Environment.NewLine + result.StackTrace
                                 });
        }

        public void SuiteStarted(TestName testName)
        {
        }

        public void SuiteFinished(NUnit.Core.TestResult result)
        {
        }

        public void UnhandledException(Exception exception)
        {
        }

        public void TestOutput(TestOutput testOutput)
        {
        }
    }
}