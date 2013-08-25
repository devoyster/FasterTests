using System;
using System.Collections.Generic;
using FasterTests.Core.Interfaces.Models;
using System.Linq;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public class AssemblySetupFixturesContext : ISetupFixturesContext
    {
        private readonly string _testAssemblyPath;
        private readonly ISetupFixtureFactory _setupFixtureFactory;
        private readonly ISetupFixtureInspector _setupFixtureInspector;
        private IList<ISetupFixture> _allFixtures;
        private Stack<ISetupFixture> _activeFixtures;

        public AssemblySetupFixturesContext(string testAssemblyPath,
                                            ISetupFixtureFactory setupFixtureFactory,
                                            ISetupFixtureInspector setupFixtureInspector)
        {
            _testAssemblyPath = testAssemblyPath;
            _setupFixtureFactory = setupFixtureFactory;
            _setupFixtureInspector = setupFixtureInspector;
        }

        public bool SetupFor(TestDescriptor test, IObserver<TestResult> resultsObserver)
        {
            InitializeSetupFixtures();

            var fixturesToTeardown = _activeFixtures.TakeWhile(f => !f.IsRequiredFor(test)).ToList();
            foreach (var fixture in fixturesToTeardown)
            {
                fixture.Teardown(resultsObserver);
                _activeFixtures.Pop();
            }

            var anyFixtureFailed = _activeFixtures.Any(f => !f.IsSetupSucceeded());

            var fixturesToSetup = _allFixtures.Where(f => f.IsRequiredFor(test))
                                              .Where(f => !_activeFixtures.Contains(f));
            foreach (var fixture in fixturesToSetup)
            {
                _activeFixtures.Push(fixture);

                if (anyFixtureFailed)
                {
                    fixture.SetParentFailed(resultsObserver);
                }
                else
                {
                    fixture.Setup(resultsObserver);
                }

                anyFixtureFailed |= !fixture.IsSetupSucceeded();
            }

            return anyFixtureFailed;
        }

        public void TeardownAll(IObserver<TestResult> resultsObserver)
        {
            if (_allFixtures == null)
            {
                return;
            }

            while (_activeFixtures.Any())
            {
                var fixture = _activeFixtures.Pop();
                fixture.Teardown(resultsObserver);
            }
        }

        private void InitializeSetupFixtures()
        {
            if (_allFixtures != null)
            {
                return;
            }

            _allFixtures = _setupFixtureInspector.LoadAllTypesFrom(_testAssemblyPath)
                                .Select(_setupFixtureFactory.Create)
                                .ToList()
                                .AsReadOnly();

            _activeFixtures = new Stack<ISetupFixture>();
        }
    }
}