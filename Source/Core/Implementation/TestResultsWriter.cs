using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Funt.Core.Models;
using Funt.Helpers;

namespace Funt.Core.Implementation
{
    public class TestResultsWriter : ITestResultsWriter
    {
        private readonly TextWriter _output;

        public TestResultsWriter(TextWriter output)
        {
            _output = output;
        }

        public void Write(IEnumerable<TestResult> results)
        {
            _output.WriteLine("Run results:");

            var sw = Stopwatch.StartNew();

            var errors = new List<string>();
            int count = 0;
            results.ForEach(r =>
                                {
                                    count++;
                                    if (r.IsSuccess)
                                    {
                                        _output.Write('.');
                                    }
                                    else if (r.IsIgnored)
                                    {
                                        _output.Write('I');
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
            _output.WriteLine("failed = {0}", errors.Count);
            _output.WriteLine();

            errors.ForEach(_output.WriteLine);
        }
    }
}