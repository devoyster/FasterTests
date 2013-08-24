using System;
using System.Collections.Generic;
using System.Reflection;
using FasterTests.Core.Interfaces.Integration.Nunit;
using FasterTests.Core.Interfaces.Models;
using System.Linq;

namespace FasterTests.Core.Integration.Nunit.SetupFixtures
{
    public class AssemblySetupFixturesContext : ISetupFixturesContext
    {
        private readonly string _testAssemblyPath;
        private readonly ISetupFixtureFactory _setupFixtureFactory;
        private IList<ISetupFixture> _allFixtures;
        private Stack<ISetupFixture> _activeFixtures;

        public AssemblySetupFixturesContext(string testAssemblyPath, ISetupFixtureFactory setupFixtureFactory)
        {
            _testAssemblyPath = testAssemblyPath;
            _setupFixtureFactory = setupFixtureFactory;
        }

        public bool SetupFor(TestDescriptor test, IObserver<TestResult> resultsObserver)
        {
            InitializeSetupFixtures();

            var fixturesToTeardown = _activeFixtures.TakeWhile(f => !f.ShouldRunFor(test)).ToList();
            foreach (var fixture in fixturesToTeardown)
            {
                fixture.Teardown(resultsObserver);
                _activeFixtures.Pop();
            }

            if (_activeFixtures.Any() && !_activeFixtures.Peek().IsSetupSucceeded)
            {
                return false;
            }

            var fixturesToSetup = _allFixtures.Where(f => f.ShouldRunFor(test))
                                              .Where(f => !_activeFixtures.Contains(f));
            foreach (var fixture in fixturesToSetup)
            {
                _activeFixtures.Push(fixture);
                fixture.Setup(resultsObserver);

                if (!fixture.IsSetupSucceeded)
                {
                    return false;
                }
            }

            return true;
        }

        public void TeardownAll(IObserver<TestResult> resultsObserver)
        {
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

            var assembly = Assembly.LoadFrom(_testAssemblyPath);
            _allFixtures = assembly.GetTypes()
                                   .Where(SetUpFixtureBuilderProvider.Instance.CanBuildFrom)
                                   .OrderBy(t => t.Namespace ?? string.Empty)
                                   .ThenBy(t => t.Name)
                                   .Select(_setupFixtureFactory.Create)
                                   .ToList()
                                   .AsReadOnly();

            _activeFixtures = new Stack<ISetupFixture>();
        }
    }
}