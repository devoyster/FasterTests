using System.Linq;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Integration;

namespace FasterTests.Core.Implementation
{
    public class TestRunnerEntryPoint : ITestRunnerEntryPoint
    {
        private readonly string _testAssemblyPath;
        private readonly ITestInspector _testInspector;
        private readonly ITestDispatcher _testDispatcher;
        private readonly ITestResultsConsumer _testResultsConsumer;

        public TestRunnerEntryPoint(string testAssemblyPath,
                                    ITestInspector testInspector,
                                    ITestDispatcher testDispatcher,
                                    ITestResultsConsumer testResultsConsumer)
        {
            _testAssemblyPath = testAssemblyPath;
            _testInspector = testInspector;
            _testDispatcher = testDispatcher;
            _testResultsConsumer = testResultsConsumer;
        }

        public void Run()
        {
            var tests = _testInspector
                            .FindAllTests(_testAssemblyPath)
                            .OrderBy(d => d.Name)
                            .ToList();

            var results = _testDispatcher.RunTestsAsync(tests);

            _testResultsConsumer.Consume(results);
        }
    }
}