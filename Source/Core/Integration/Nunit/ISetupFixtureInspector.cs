using System;
using System.Collections.Generic;

namespace FasterTests.Core.Integration.Nunit
{
    public interface ISetupFixtureInspector
    {
        IEnumerable<Type> LoadAllTypesFrom(string assemblyPath);
    }
}