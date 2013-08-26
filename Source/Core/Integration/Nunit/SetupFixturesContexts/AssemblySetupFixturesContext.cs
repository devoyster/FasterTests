using System;
using System.Collections.Generic;
using FasterTests.Core.Interfaces.Models;
using System.Linq;
using FasterTests.Helpers.Collections;

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

            _allFixturesLazy = new Lazy<IEnumerable<ISetupFixture>>(CreateAllFixtures);
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

        private IEnumerable<ISetupFixture> CreateAllFixtures()
        {
            return _setupFixtureFactory.CreateAllFrom(_testAssemblyPath)
                    .GroupBy(f => f.Type.Namespace)
                    .Select(g => g.First());
        }

        private void TeardownNotRequiredFixtures(TestDescriptor test, IObserver<TestResult> resultsObserver)
        {
            var notRequiredFixtures = AllFixtures.Except(GetRequiredFixtures(test));
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