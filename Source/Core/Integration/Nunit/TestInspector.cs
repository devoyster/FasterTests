using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FasterTests.Core.Interfaces.Integration;
using FasterTests.Core.Interfaces.Models;
using NUnit.Core;

namespace FasterTests.Core.Integration.Nunit
{
    public class TestInspector : ITestInspector
    {
        private readonly ITestFrameworkInitializer _initializer;
        private readonly ISetupFixtureTypeInspector _setupFixtureTypeInspector;

        public TestInspector(ITestFrameworkInitializer initializer, ISetupFixtureTypeInspector setupFixtureTypeInspector)
        {
            _initializer = initializer;
            _setupFixtureTypeInspector = setupFixtureTypeInspector;
        }

        public IEnumerable<TestDescriptor> LoadAllTestsFrom(string assemblyPath)
        {
            _initializer.EnsureInitialized();

            var assembly = Assembly.LoadFrom(assemblyPath);

            return assembly
                    .GetTypes()
                    .Where(TestFixtureBuilder.CanBuildFrom)
                    .Except(_setupFixtureTypeInspector.LoadAllFrom(assemblyPath))
                    .Select(t => new TestDescriptor
                                    {
                                        Name = t.FullName,
                                        AssemblyPath = assembly.Location
                                    });
        }
    }
}