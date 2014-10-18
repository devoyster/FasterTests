using System.Linq;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Integration;

namespace FasterTests.Core
{
    public class TestRunnerBootstrapper : ITestRunnerBootstrapper
    {
        private readonly string _testAssemblyPath;
        private readonly ITestInspector _testInspector;
        private readonly ITestDispatcher _testDispatcher;
        private readonly ITestResultsConsumer _testResultsConsumer;

        public TestRunnerBootstrapper(TestRunSettings settings,
                                      ITestInspector testInspector,
                                      ITestDispatcher testDispatcher,
                                      ITestResultsConsumer testResultsConsumer)
        {
            _testAssemblyPath = settings.AssemblyPath;
            _testInspector = testInspector;
            _testDispatcher = testDispatcher;
            _testResultsConsumer = testResultsConsumer;
        }

        public void Run()
        {
            var tests = _testInspector
                            .LoadAllTestsFrom(_testAssemblyPath)
                            .OrderBy(d => d.Name)
                            .ToList();

            var results = _testDispatcher.RunTests(tests);

            _testResultsConsumer.Consume(results);
        }
    }
}