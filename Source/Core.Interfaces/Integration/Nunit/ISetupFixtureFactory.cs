using System;

namespace FasterTests.Core.Interfaces.Integration.Nunit
{
    public interface ISetupFixtureFactory
    {
        ISetupFixture Create(Type type);
    }
}