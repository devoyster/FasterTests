using System;
using System.Reactive.Subjects;
using System.Reflection;
using FasterTests.Core.Interfaces.Integration;
using FasterTests.Core.Interfaces.Models;
using System.Reactive.Linq;
using NUnit.Core;
using TestResult = FasterTests.Core.Interfaces.Models.TestResult;

namespace FasterTests.Core.Integration.Nunit
{
    public class TestEngine : ITestEngine
    {
        private readonly ITestFrameworkInitializer _initializer;
        private readonly ISetupFixturesContext _setupFixturesContext;
        private readonly ITestFilterProvider _testFilterProvider;
        private readonly Subject<TestResult> _results;

        public TestEngine(ITestFrameworkInitializer initializer,
                          ISetupFixturesContext setupFixturesContext,
                          ITestFilterProvider testFilterProvider)
        {
            _initializer = initializer;
            _setupFixturesContext = setupFixturesContext;
            _testFilterProvider = testFilterProvider;
            _results = new Subject<TestResult>();
        }

        public IObservable<TestResult> Results
        {
            get { return _results.AsObservable(); }
        }

        public void RunTest(TestDescriptor test)
        {
            _initializer.EnsureInitialized();

            var testType = Assembly.LoadFrom(test.AssemblyPath).GetType(test.Name);
            var testFixture = TestFixtureBuilder.BuildFrom(testType);

            if (!_testFilterProvider.TestFilter.Pass(testFixture))
            {
                return;
            }

            if (_setupFixturesContext.SetupFor(test, _results))
            {
                testFixture.Run(new ObserverEventListener(_results), _testFilterProvider.TestFilter);
            }
            else
            {
#pragma warning disable 168
                foreach (ITest testMethod in testFixture.Tests)
#pragma warning restore 168
                {
                    _results.OnNext(new TestResult
                                        {
                                            IsSuccess = false,
                                            ErrorMessage = "TestFixtureSetUp failed"
                                        });
                }
            }
        }

        public void Dispose()
        {
            _setupFixturesContext.TeardownAll(_results);
            _results.OnCompleted();
        }
    }
}