using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Funt.Core.Models;

namespace Funt.Core
{
    public class TestDispatcher
    {
        public IEnumerable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests)
        {
            return Observable
                    .Create<TestResult>(o => SplitAndRunTests(tests, o))
                    .SubscribeOn(Scheduler.Default)
                    .ToEnumerable();
        }

        private IDisposable SplitAndRunTests(IEnumerable<TestDescriptor> tests, IObserver<TestResult> observer)
        {
            foreach (var test in tests)
            {
                observer.OnNext(new TestResult
                                    {
                                        Test = test,
                                        IsSuccess = true
                                    });
            }

            observer.OnCompleted();
            return Disposable.Empty;
        }
    }
}