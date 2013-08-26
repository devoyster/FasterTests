using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using System.Linq;
using FasterTests.Helpers.Collections;

namespace FasterTests.Core.Workers
{
    public class AppDomainWorker : MarshalByRefObject
    {
        public void InstallAssemblyResolve(string parentDomainBinPath)
        {
            AppDomain.CurrentDomain.AssemblyResolve +=
                (_, e) =>
                    {
                        var name = new AssemblyName(e.Name).Name;
                        try
                        {
                            return Assembly.LoadFrom(Path.Combine(parentDomainBinPath, name) + ".dll");
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
            var engine = new TestEngine(new TestFrameworkInitializer(),
                                        new AssemblySetupFixturesContext(tests.First().AssemblyPath, new SetupFixtureFactory(new SetupFixtureTypeInspector(), new SetupFixtureAdapterFactory())));

            using (engine)
            {
                engine.Results.Subscribe(observer.OnNext);

                tests.ForEach(engine.RunTest);
            }

            observer.OnCompleted();
            return Disposable.Create(() => AppDomain.Unload(AppDomain.CurrentDomain));
        }
    }
}