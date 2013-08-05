using System;
using System.Linq;
using Funt.Core.Nunit;
using Funt.Core.Workers;

namespace Funt.Core
{
    public class TestRunner
    {
        private readonly string _testAssemblyPath;
        private readonly string[][] _noParallelGroups;
        private readonly string[] _configStringsToPatch;

        public TestRunner(string testAssemblyPath, string[][] noParallelGroups, string[] configStringsToPatch)
        {
            _testAssemblyPath = testAssemblyPath;
            _noParallelGroups = noParallelGroups;
            _configStringsToPatch = configStringsToPatch;
        }

        public void Run()
        {
            var inspector = new NunitTestInspector();
            var tests = inspector
                            .FindAllTests(_testAssemblyPath)
                            .OrderBy(d => d.Name)
                            .ToList();

            var dispatcher = new TestDispatcher(new TestWorkersPool(_configStringsToPatch), _noParallelGroups);
            var results = dispatcher.RunTestsAsync(tests);

            var writer = new TestResultsWriter(Console.Out);
            writer.Write(results);
        }
    }
}