﻿using System.Collections.Generic;
using System.IO;
using FasterTests.Core;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Models;
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

        It should_write_count_of_all_executed_tests = () => result.ShouldContain("Tests run: 3,");

        It should_write_that_zero_tests_were_failed = () => result.ShouldContain("Failures: 0,");

        It should_write_that_zero_tests_were_ignored = () => result.ShouldContain("Not run: 0, Ignored: 0");

        private static StringWriter output;
        private static TestResultsConsoleWriter writer;
        private static IEnumerable<TestResult> testResults;
        private static string result;
    }
}