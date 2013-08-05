using System;
using Funt.Core.Implementation;
using Funt.Core.Integration.Implementation.Nunit;
using Funt.Core.Workers.Implementation;

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
            var dispatcher = new TestDispatcher(new TestWorkersPool(_configStringsToPatch), _noParallelGroups);
            var writer = new TestResultsConsoleWriter(Console.Out);

            var entryPoint = new TestRunnerEntryPoint(_testAssemblyPath, inspector, dispatcher, writer);

            entryPoint.Run();
        }
    }
}