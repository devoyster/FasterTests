using System.Collections.Generic;
using System.IO;
using System.Linq;
using FasterTests.Core.Interfaces;
using NDesk.Options;

namespace FasterTests.ConsoleRunner
{
    public class TestRunnerCommand
    {
        private readonly ITestRunner _testRunner;
        private readonly TestRunSettings _settings;
        private bool _showHelp;

        public TestRunnerCommand(ITestRunner testRunner)
        {
            _testRunner = testRunner;
            _settings = new TestRunSettings();
        }

        public void Execute(IEnumerable<string> args, TextWriter output)
        {
            var options = ConfigureOptions();

            ConfigureOptions().Parse(args);

            if (_showHelp)
            {
                WriteHelp(options, output);
                return;
            }

            if (string.IsNullOrWhiteSpace(_settings.AssemblyPath))
            {
                output.WriteLine("Error: Test assembly path should be provided as first argument. See help below. Exiting...");
                output.WriteLine();
                WriteHelp(options, output);
                return;
            }

            _settings.Output = output;
            _testRunner.Run(_settings);
        }

        private OptionSet ConfigureOptions()
        {
            return new OptionSet
                {
                    { "<>", v => _settings.AssemblyPath = v },
                    { "g|groups=", ";-separated namespace groups", v => _settings.NoParallelGroups = ParseGroups(v) },
                    { "c|config=", ",-separated config strings to patch", v => _settings.ConfigStringsToPatch = v.Split(',') },
                    { "h|help", "Show this message and exit", h => _showHelp = h != null }
                };
        }

        private static string[][] ParseGroups(string groupsString)
        {
            return groupsString
                .Split(';')
                .Select(g => g.Split(','))
                .ToArray();
        }

        private void WriteHelp(OptionSet options, TextWriter output)
        {
            output.WriteLine("Usage: FasterTests-Run \"test assembly path\" [options]");
            output.WriteLine("Runs unit tests in parallel");
            output.WriteLine();
            output.WriteLine("Example: FasterTests-Run MyTestAssembly.dll");
            output.WriteLine();
            output.WriteLine("Options:");

            options.WriteOptionDescriptions(output);
        }
    }
}