using System;
using System.Collections.Generic;
using System.Linq;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public class SetupFixtureFactory : ISetupFixtureFactory
    {
        private readonly ISetupFixtureTypeInspector _setupFixtureTypeInspector;

        public SetupFixtureFactory(ISetupFixtureTypeInspector setupFixtureTypeInspector)
        {
            _setupFixtureTypeInspector = setupFixtureTypeInspector;
        }

        public ISetupFixture Create(Type type)
        {
            return new SetupFixture(type);
        }

        public IEnumerable<ISetupFixture> CreateAllFrom(string assemblyPath)
        {
            return _setupFixtureTypeInspector.LoadAllFrom(assemblyPath)
                    .Select(Create)
                    .ToList();
        }
    }
}