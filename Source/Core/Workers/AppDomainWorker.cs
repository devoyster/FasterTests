using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using Funt.Core.Models;

namespace Funt.Core.Workers
{
    public class AppDomainWorker : MarshalByRefObject
    {
        public IObservable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests)
        {
            return Observable
                       .Create<TestResult>(o => RunTests(tests, o))
                       .SubscribeOn(Scheduler.Default)
                       .Remotable();
        }

        private IDisposable RunTests(IEnumerable<TestDescriptor> tests, IObserver<TestResult> observer)
        {
            var random = new Random();

            foreach (var test in tests)
            {
                observer.OnNext(new TestResult
                                    {
                                        Test = test,
                                        IsSuccess = random.Next(20) != 0
                                    });

                Thread.Sleep(random.Next(100));
            }

            observer.OnCompleted();

            return Disposable.Create(() => AppDomain.Unload(AppDomain.CurrentDomain));
        }
    }
}