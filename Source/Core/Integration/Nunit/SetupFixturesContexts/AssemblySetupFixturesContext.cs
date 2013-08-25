using System;
using System.Collections.Generic;
using FasterTests.Core.Interfaces.Models;
using System.Linq;
using FasterTests.Helpers;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public class AssemblySetupFixturesContext : ISetupFixturesContext
    {
        private readonly string _testAssemblyPath;
        private readonly ISetupFixtureFactory _setupFixtureFactory;
        private readonly Lazy<IEnumerable<ISetupFixture>> _allFixturesLazy;

        public AssemblySetupFixturesContext(string testAssemblyPath,
                                            ISetupFixtureFactory setupFixtureFactory)
        {
            _testAssemblyPath = testAssemblyPath;
            _setupFixtureFactory = setupFixtureFactory;

            _allFixturesLazy = new Lazy<IEnumerable<ISetupFixture>>(() =>
                _setupFixtureFactory.CreateAllFrom(_testAssemblyPath).ToReadOnlyCollection());
        }

        public bool SetupFor(TestDescriptor test, IObserver<TestResult> resultsObserver)
        {
            TeardownNotRequiredFixtures(test, resultsObserver);

            var fixturesWhereSetupExecuted = GetRequiredFixtures(test).ToLookup(f => f.IsSetupExecuted());

            var anyFixtureFailed = fixturesWhereSetupExecuted[true].Any(f => f.IsSetupFailed());
            foreach (var fixture in fixturesWhereSetupExecuted[false])
            {
                if (anyFixtureFailed)
                {
                    fixture.SetParentFailed(resultsObserver);
                }
                else
                {
                    fixture.Setup(resultsObserver);
                }

                anyFixtureFailed |= fixture.IsSetupFailed();
            }

            return !anyFixtureFailed;
        }

        public void TeardownAll(IObserver<TestResult> resultsObserver)
        {
            TeardownExecutedFixtures(AllFixtures, resultsObserver);
        }

        private IEnumerable<ISetupFixture> AllFixtures
        {
            get { return _allFixturesLazy.Value; }
        }

        private void TeardownNotRequiredFixtures(TestDescriptor test, IObserver<TestResult> resultsObserver)
        {
            var notRequiredFixtures = AllFixtures.Where(f => !f.IsRequiredFor(test));
            TeardownExecutedFixtures(notRequiredFixtures, resultsObserver);
        }

        private void TeardownExecutedFixtures(IEnumerable<ISetupFixture> notRequiredFixtures, IObserver<TestResult> resultsObserver)
        {
            notRequiredFixtures
                .Where(f => f.IsSetupExecuted())
                .Reverse()
                .ForEach(f => f.Teardown(resultsObserver));
        }

        private IEnumerable<ISetupFixture> GetRequiredFixtures(TestDescriptor test)
        {
            return AllFixtures.Where(f => f.IsRequiredFor(test));
        }
    }
}