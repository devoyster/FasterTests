using System;
using System.Collections.Generic;

namespace FasterTests.Core.Integration.Nunit
{
    public interface ISetupFixtureTypeInspector
    {
        IEnumerable<Type> LoadAllFrom(string assemblyPath);
    }
}