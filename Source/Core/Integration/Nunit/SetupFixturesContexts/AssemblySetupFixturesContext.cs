using System;
using System.Collections.Generic;
using System.Threading;
using FasterTests.Core.Interfaces.Models;
using System.Linq;
using FasterTests.Helpers.Collections;
using FasterTests.Helpers.Trees;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public class AssemblySetupFixturesContext : ISetupFixturesContext
    {
        private readonly Lazy<ITree<ISetupFixture>> _lazyFixtures;

        public AssemblySetupFixturesContext(string testAssemblyPath,
                                            ISetupFixtureTreeBuilder setupFixtureTreeBuilder)
        {
            _lazyFixtures = new Lazy<ITree<ISetupFixture>>(
                () => setupFixtureTreeBuilder.BuildFrom(testAssemblyPath),
                LazyThreadSafetyMode.None);
        }

        public bool SetupFor(TestDescriptor test, IObserver<TestResult> resultsObserver)
        {
            var requiredFixtures = GetRequiredFixtures(test);

            TeardownNotRequiredFixtures(requiredFixtures, resultsObserver);

            var fixturesWhereSetupExecuted = requiredFixtures.ToLookup(f => f.IsSetupExecuted());

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
            TeardownNotRequiredFixtures(Enumerable.Empty<ISetupFixture>(), resultsObserver);
        }

        private ITree<ISetupFixture> Fixtures
        {
            get { return _lazyFixtures.Value; }
        }

        private void TeardownNotRequiredFixtures(IEnumerable<ISetupFixture> requiredFixtures, IObserver<TestResult> resultsObserver)
        {
            GetFixturesWhereSetupExecuted()
                .Except(requiredFixtures)
                .Reverse()
                .ForEach(f => f.Teardown(resultsObserver));
        }

        private IEnumerable<ISetupFixture> GetFixturesWhereSetupExecuted()
        {
            return Fixtures.ToBranchWhile(f => f.IsSetupExecuted());
        }

        private ICollection<ISetupFixture> GetRequiredFixtures(TestDescriptor test)
        {
            return Fixtures.ToBranchWhile(f => f.IsRequiredFor(test)).ToList();
        }
    }
}