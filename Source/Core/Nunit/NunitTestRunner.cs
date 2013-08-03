using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using Funt.Core.Models;
using System.Linq;
using NUnit.Core;
using NUnit.Core.Builders;
using NUnit.Core.Filters;

using TestResult = Funt.Core.Models.TestResult;

namespace Funt.Core.Nunit
{
    public class NunitTestRunner
    {
        public IObservable<TestResult> RunTests(IEnumerable<TestDescriptor> tests)
        {
            return Observable.Create<TestResult>(
                observer =>
                    {
                        if (!CoreExtensions.Host.Initialized)
                        {
                            CoreExtensions.Host.InitializeService();
                        }

                        var assemblyPath = tests.First().AssemblyPath;
                        var assembly = Assembly.LoadFrom(assemblyPath);

                        var allNamespaces = tests.Select(d => d.Name.Substring(0, d.Name.LastIndexOf('.'))).Distinct();

                        var setUpFixtureBuilder = new SetUpFixtureBuilder();
                        var allSetUpFixtureNames = assembly
                                                    .GetTypes()
                                                    .Where(t => string.IsNullOrEmpty(t.Namespace) || allNamespaces.Any(n => n.Contains(t.Namespace)))
                                                    .Where(setUpFixtureBuilder.CanBuildFrom)
                                                    .Select(t => t.FullName);

                        var allTypeNames = tests.Select(d => d.Name).Union(allSetUpFixtureNames).OrderBy(n => n);

                        var fixtures = allTypeNames.Select(assembly.GetType).Select(TestFixtureBuilder.BuildFrom).ToList();

                        TestSuite testAssembly = new TestAssembly(assembly, assemblyPath);
                        var treeBuilder = new NamespaceTreeBuilder(testAssembly);
                        treeBuilder.Add(fixtures);
                        testAssembly = treeBuilder.RootSuite;
                        testAssembly.Sort();

                        var testPackage = new TestPackage(assemblyPath);
                        TestExecutionContext.CurrentContext.TestPackage = testPackage;

                        // TODO: Move to settings
                        var testFilter = new NotFilter(new CategoryFilter("Slow"), topLevel: true);
                        testAssembly.Run(new ObserverEventListener(observer), testFilter);

                        observer.OnCompleted();
                        return Disposable.Empty;
                    });
        }
    }

    public class ObserverEventListener : EventListener
    {
        private readonly IObserver<TestResult> _observer;

        public ObserverEventListener(IObserver<TestResult> observer)
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
                                     Test = new TestDescriptor
                                                {
                                                    Name = result.FullName
                                                },
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