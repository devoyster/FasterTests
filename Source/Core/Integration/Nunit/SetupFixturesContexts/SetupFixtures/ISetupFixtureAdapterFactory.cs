using System;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public interface ISetupFixtureAdapterFactory
    {
        ISetupFixtureAdapter Create(Type type);
    }
}