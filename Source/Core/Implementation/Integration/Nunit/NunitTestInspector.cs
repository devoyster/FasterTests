using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FasterTests.Core.Interfaces.Integration;
using FasterTests.Core.Interfaces.Models;
using NUnit.Core;
using NUnit.Core.Builders;

namespace FasterTests.Core.Implementation.Integration.Nunit
{
    public class NunitTestInspector : ITestInspector
    {
        public IEnumerable<TestDescriptor> LoadAllTestsFrom(string assemblyPath)
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