using System;

namespace FasterTests.Core.Integration.Nunit
{
    public interface ISetupFixtureFactory
    {
        ISetupFixture Create(Type type);
    }
}