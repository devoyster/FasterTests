using System;
using System.Collections.Generic;
using System.Linq;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public class SetupFixtureFactory : ISetupFixtureFactory
    {
        private readonly ISetupFixtureTypeInspector _setupFixtureTypeInspector;
        private readonly ISetupFixtureAdapterFactory _setupFixtureAdapterFactory;

        public SetupFixtureFactory(ISetupFixtureTypeInspector setupFixtureTypeInspector,
                                   ISetupFixtureAdapterFactory setupFixtureAdapterFactory)
        {
            _setupFixtureTypeInspector = setupFixtureTypeInspector;
            _setupFixtureAdapterFactory = setupFixtureAdapterFactory;
        }

        public ISetupFixture Create(Type type)
        {
            return new SetupFixture(type, _setupFixtureAdapterFactory);
        }

        public IEnumerable<ISetupFixture> CreateAllFrom(string assemblyPath)
        {
            return _setupFixtureTypeInspector.LoadAllFrom(assemblyPath)
                    .Select(Create)
                    .ToList();
        }
    }
}