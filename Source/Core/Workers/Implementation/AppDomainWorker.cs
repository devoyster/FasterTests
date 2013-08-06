using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using FasterTests.Core.Integration.Implementation.Nunit;
using FasterTests.Core.Models;

namespace FasterTests.Core.Workers.Implementation
{
    public class AppDomainWorker : MarshalByRefObject
    {
        public void InstallAssemblyResolve()
        {
            var funtBinariesPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            AppDomain.CurrentDomain.AssemblyResolve +=
                (_, e) =>
                    {
                        var name = new AssemblyName(e.Name).Name;
                        try
                        {
                            return Assembly.LoadFrom(Path.Combine(funtBinariesPath, name) + ".dll");
                        }
                        catch
                        {
                        }

                        return null;
                    };
        }

        public IObservable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests)
        {
            return Observable
                       .Create<TestResult>(o => RunTests(tests, o))
                       .SubscribeOn(Scheduler.Default)
                       .Remotable();
        }

        private IDisposable RunTests(IEnumerable<TestDescriptor> tests, IObserver<TestResult> observer)
        {
            var nunitRunner = new NunitTestEngine();

            nunitRunner
                .RunTests(tests)
                .Subscribe(observer.OnNext);

            observer.OnCompleted();
            return Disposable.Create(() => AppDomain.Unload(AppDomain.CurrentDomain));
        }
    }
}