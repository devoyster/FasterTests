using System.Collections.Generic;
using System.IO;
using FasterTests.Core.Implementation;
using FasterTests.Core.Interfaces.Models;
using Machine.Specifications;

namespace FasterTests.Tests.Core.TestResultsConsoleWriterSpecs
{
    [Subject(typeof(TestResultsConsoleWriter))]
    public class When_writing_ignored_test_results_to_console
    {
        Establish context = () =>
        {
            output = new StringWriter();
            writer = new TestResultsConsoleWriter(output);

            testResults = new[]
                              {
                                  new TestResult { IsIgnored = true },
                                  new TestResult { IsSuccess = true },
                                  new TestResult { IsIgnored = true }
                              };
        };

        Because of = () =>
        {
            writer.Consume(testResults);
            result = output.ToString();
        };

        It should_write_progress_dots_for_all_tests = () => result.ShouldContain("I.I");

        It should_write_count_of_all_tests = () => result.ShouldContain("count = 3");

        It should_write_that_zero_tests_were_failed = () => result.ShouldContain("failed = 0");

        private static StringWriter output;
        private static TestResultsConsoleWriter writer;
        private static IEnumerable<TestResult> testResults;
        private static string result;
    }
}