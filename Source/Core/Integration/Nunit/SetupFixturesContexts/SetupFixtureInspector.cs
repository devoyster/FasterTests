using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Core.Builders;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public class SetupFixtureInspector : ISetupFixtureInspector
    {
        public IEnumerable<Type> LoadAllTypesFrom(string assemblyPath)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);

            var setUpFixtureBuilder = new SetUpFixtureBuilder();

            return assembly.GetTypes()
                    .Where(setUpFixtureBuilder.CanBuildFrom)
                    .OrderBy(t => t.Namespace ?? string.Empty)
                    .ThenBy(t => t.Name)
                    .ToList();
        }
    }
}