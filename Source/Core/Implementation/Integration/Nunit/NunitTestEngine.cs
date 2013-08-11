using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using FasterTests.Core.Interfaces.Integration;
using FasterTests.Core.Interfaces.Models;
using NUnit.Core;
using NUnit.Core.Builders;
using NUnit.Core.Filters;

using TestResult = FasterTests.Core.Interfaces.Models.TestResult;

namespace FasterTests.Core.Implementation.Integration.Nunit
{
    public class NunitTestEngine : ITestEngine
    {
        public IObservable<TestResult> RunTests(IEnumerable<TestDescriptor> tests)
        {
            return Observable.Create<TestResult>(
                observer =>
                    {
                        NunitInitializer.EnsureInitialized();

                        var assemblyPath = tests.First().AssemblyPath;
                        var assembly = Assembly.LoadFrom(assemblyPath);

                        var allNamespaces = tests.Select(d => d.Name.Substring(0, d.Name.LastIndexOf('.'))).Distinct();

                        var setUpFixtureBuilder = new SetUpFixtureBuilder();
                        var allSetUpFixtureNames = assembly
                                                    .GetTypes()
                                                    .Where(t => string.IsNullOrEmpty(t.Namespace) || allNamespaces.Any(n => n.StartsWith(t.Namespace)))
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
                        testAssembly.Run(new NunitObserverEventListener(observer), testFilter);

                        observer.OnCompleted();
                        return Disposable.Empty;
                    });
        }
    }
}