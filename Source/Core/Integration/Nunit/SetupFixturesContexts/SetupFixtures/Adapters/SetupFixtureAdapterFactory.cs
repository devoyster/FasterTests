using System;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters
{
    public class SetupFixtureAdapterFactory : ISetupFixtureAdapterFactory
    {
        public ISetupFixtureAdapter Create(Type type)
        {
            return new SetupFixtureAdapter(type);
        }
    }
}