using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Funt.Core.Models;
using NUnit.Core;
using NUnit.Core.Builders;

namespace Funt.Core.Integration.Implementation.Nunit
{
    public class NunitTestInspector : ITestInspector
    {
        public IEnumerable<TestDescriptor> FindAllTests(string assemblyPath)
        {
            NunitInitializer.EnsureInitialized();

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