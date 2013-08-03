using System.Collections.Generic;
using System.Reflection;
using Funt.Core.Models;
using System.Linq;
using NUnit.Core;
using NUnit.Core.Builders;

namespace Funt.Core.Nunit
{
    public class NunitTestInspector
    {
        public IEnumerable<TestDescriptor> FindAllTests(string assemblyPath)
        {
            if (!CoreExtensions.Host.Initialized)
            {
                CoreExtensions.Host.InitializeService();
            }

            var assembly = Assembly.LoadFrom(assemblyPath);

            var setUpFixtureBuilder = new SetUpFixtureBuilder();
            return assembly
                    .GetTypes()
                    .Where(TestFixtureBuilder.CanBuildFrom)
                    .Where(t => !setUpFixtureBuilder.CanBuildFrom(t))
                    .Select(t => new TestDescriptor
                                    {
                                        Name = t.FullName,
                                        AssemblyPath = assembly.Location
                                    });
        }
    }
}