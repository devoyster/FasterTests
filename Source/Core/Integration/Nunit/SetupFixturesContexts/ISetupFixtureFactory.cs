using System;
using System.Collections.Generic;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public interface ISetupFixtureFactory
    {
        ISetupFixture Create(Type type);

        IEnumerable<ISetupFixture> CreateAllFrom(string assemblyPath);
    }
}