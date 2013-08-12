using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FasterTests.Core.Implementation.Integration.Nunit.SetupFixtures;
using FasterTests.Core.Interfaces.Integration;
using FasterTests.Core.Interfaces.Models;
using NUnit.Core;

namespace FasterTests.Core.Implementation.Integration.Nunit
{
    public class TestInspector : ITestInspector
    {
        private readonly ITestFrameworkInitializer _initializer;

        public TestInspector(ITestFrameworkInitializer initializer)
        {
            _initializer = initializer;
        }

        public IEnumerable<TestDescriptor> LoadAllTestsFrom(string assemblyPath)
        {
            _initializer.EnsureInitialized();

            var assembly = Assembly.LoadFrom(assemblyPath);

            return assembly
                    .GetTypes()
                    .Where(TestFixtureBuilder.CanBuildFrom)
                    .Where(t => !SetUpFixtureBuilderProvider.Instance.CanBuildFrom(t))
                    .Select(t => new TestDescriptor
                                    {
                                        Name = t.FullName,
                                        AssemblyPath = assembly.Location
                                    });
        }
    }
}