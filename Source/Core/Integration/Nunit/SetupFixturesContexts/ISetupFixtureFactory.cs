using System;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public interface ISetupFixtureFactory
    {
        ISetupFixture Create(Type type);
    }
}