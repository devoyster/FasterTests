using System.Collections.Generic;
using System.IO;
using FasterTests.Core;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Core.Interfaces.Settings;
using Machine.Specifications;

namespace FasterTests.Tests.Core.TestResultsConsoleWriterSpecs
{
    [Subject(typeof(TestResultsConsoleWriter))]
    public class When_writing_successful_test_results_to_console
    {
        Establish context = () =>
        {
            output = new StringWriter();
            writer = new TestResultsConsoleWriter(new TestRunSettings { Output = output });

            testResults = new[]
                              {
                                  new TestResult { IsSuccess = true },
                                  new TestResult { IsSuccess = true },
                                  new TestResult { IsSuccess = true }
                              };
        };

        Because of = () =>
        {
            writer.Consume(testResults);
            result = output.ToString();
        };

        It should_write_progress_dots_for_all_tests = () => result.ShouldContain("...");

        It should_write_count_of_all_tests = () => result.ShouldContain("count = 3");

        It should_write_that_zero_tests_were_failed = () => result.ShouldContain("failed = 0");

        private static StringWriter output;
        private static TestResultsConsoleWriter writer;
        private static IEnumerable<TestResult> testResults;
        private static string result;
    }
}