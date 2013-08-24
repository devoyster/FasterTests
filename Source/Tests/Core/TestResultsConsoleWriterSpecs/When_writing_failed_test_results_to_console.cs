using System.Collections.Generic;
using System.IO;
using FasterTests.Core;
using FasterTests.Core.Interfaces.Models;
using Machine.Specifications;

namespace FasterTests.Tests.Core.TestResultsConsoleWriterSpecs
{
    [Subject(typeof(TestResultsConsoleWriter))]
    public class When_writing_failed_test_results_to_console
    {
        Establish context = () =>
        {
            output = new StringWriter();
            writer = new TestResultsConsoleWriter(output);

            testResults = new[]
                              {
                                  new TestResult(),
                                  new TestResult { IsSuccess = true },
                                  new TestResult()
                              };
        };

        Because of = () =>
        {
            writer.Consume(testResults);
            result = output.ToString();
        };

        It should_write_progress_dots_for_all_tests = () => result.ShouldContain("F.F");

        It should_write_count_of_all_tests = () => result.ShouldContain("count = 3");

        It should_write_that_two_tests_were_failed = () => result.ShouldContain("failed = 2");

        private static StringWriter output;
        private static TestResultsConsoleWriter writer;
        private static IEnumerable<TestResult> testResults;
        private static string result;
    }
}