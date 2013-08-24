using System;

namespace FasterTests.Core.Integration.Nunit.SetupFixtures
{
    public class SetupFixtureFactory : ISetupFixtureFactory
    {
        public ISetupFixture Create(Type type)
        {
            return new SetupFixture(type);
        }
    }
}