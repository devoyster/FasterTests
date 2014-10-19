using System.Collections.Generic;
using System.Diagnostics;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Helpers.Collections;
using System.Linq;

namespace FasterTests.Core
{
    public class TestResultsConsoleWriter : ITestResultsConsumer
    {
        private readonly TestRunSettings _settings;

        public TestResultsConsoleWriter(TestRunSettings settings)
        {
            _settings = settings;
        }

        public void Consume(IEnumerable<TestResult> results)
        {
            var output = _settings.Output;

            output.WriteLine("Running \"{0}\":", _settings.AssemblyPath);

            var sw = Stopwatch.StartNew();

            var errors = new List<string>();
            
            int totalCount = 0;
            int ignoredCount = 0;

            results.ForEach(r =>
                                {
                                    totalCount++;
                                    if (r.IsSuccess)
                                    {
                                        output.Write('.');
                                    }
                                    else if (r.IsIgnored)
                                    {
                                        output.Write('N');
                                        ignoredCount++;
                                    }
                                    else
                                    {
                                        output.Write('F');
                                        errors.Add(r.ErrorMessage);
                                    }
                                });

            sw.Stop();

            output.WriteLine();
            output.WriteLine("Tests run: {0}, Failures: {1}, Time: {2:R}", totalCount - ignoredCount, errors.Count, sw.Elapsed.TotalSeconds);
            output.WriteLine("  Not run: {0}, Ignored: {0}", ignoredCount);

            if (errors.Any())
            {
                output.WriteLine();
                output.WriteLine("Failures:");

                foreach (var x in errors.Select((error, i) => new { error, i }))
                {
                    output.WriteLine("{0}) {1}", x.i + 1, x.error);
                    output.WriteLine();
                }
            }
        }
    }
}