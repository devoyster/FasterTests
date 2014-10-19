using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Helpers.Collections;

namespace FasterTests.Core
{
    public class TestResultsConsoleWriter : ITestResultsConsumer
    {
        private readonly TextWriter _output;

        public TestResultsConsoleWriter(TestRunSettings settings)
        {
            _output = settings.Output;
        }

        public void Consume(IEnumerable<TestResult> results)
        {
            _output.WriteLine("Run results:");

            var sw = Stopwatch.StartNew();

            var errors = new List<string>();
            
            int count = 0;
            int successfulCount = 0;
            int ignoredCount = 0;

            results.ForEach(r =>
                                {
                                    count++;
                                    if (r.IsSuccess)
                                    {
                                        _output.Write('.');
                                        successfulCount++;
                                    }
                                    else if (r.IsIgnored)
                                    {
                                        _output.Write('I');
                                        ignoredCount++;
                                    }
                                    else
                                    {
                                        _output.Write('F');
                                        errors.Add(r.ErrorMessage);
                                    }
                                });

            sw.Stop();

            _output.WriteLine();
            _output.WriteLine();
            _output.WriteLine("time = {0}s", sw.Elapsed.TotalSeconds);
            _output.WriteLine("count = {0}", count);
            _output.WriteLine();
            _output.WriteLine("successful = {0}", successfulCount);
            _output.WriteLine("failed = {0}", errors.Count);
            _output.WriteLine("ignored = {0}", ignoredCount);
            _output.WriteLine();

            errors.ForEach(_output.WriteLine);
        }
    }
}