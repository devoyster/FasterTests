﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FasterTests.Core.Interfaces;
using NDesk.Options;

namespace FasterTests.ConsoleRunner
{
    public class TestRunnerCommand
    {
        private readonly ITestRunner _testRunner;
        private readonly TestRunSettings _settings;
        private bool _showHelp;
        private int? _degreeOfParallelism;

        public TestRunnerCommand(ITestRunner testRunner)
        {
            _testRunner = testRunner;
            _settings = new TestRunSettings();
        }

        public void Execute(IEnumerable<string> args, TextWriter output)
        {
            var options = ConfigureOptions();
            options.Parse(args);

            WriteProgramDescription(output);

            if (_showHelp)
            {
                WriteHelp(options, output);
                return;
            }

            if (string.IsNullOrWhiteSpace(_settings.AssemblyPath))
            {
                output.WriteLine();
                output.WriteLine("Error: Test assembly path should be provided as first argument. See help below. Exiting...");
                WriteHelp(options, output);
                return;
            }

            _settings.DegreeOfParallelism = _degreeOfParallelism ?? Environment.ProcessorCount;
            _settings.Output = output;

            _testRunner.Run(_settings);
        }

        private OptionSet ConfigureOptions()
        {
            return new OptionSet
                {
                    { "<>", v => _settings.AssemblyPath = v },
                    { "i|IncludeCategories=", "Comma-separated categories to include", v => _settings.IncludeCategories = ParseCsv(v) },
                    { "x|ExcludeCategories=", "Comma-separated categories to exclude", v => _settings.ExcludeCategories = ParseCsv(v) },
                    { "p|DegreeOfParallelism=", "Max amount of tests to run in parallel", (int v) => _degreeOfParallelism = v },
                    { "g|NoParallelGroups=", "Semicolon-separated namespace groups", v => _settings.NoParallelGroups = ParseGroups(v) },
                    { "c|ConfigStringsToPatch=", "Comma-separated config strings to patch", v => _settings.ConfigStringsToPatch = ParseCsv(v) },
                    { "h|Help", "Show this message and exit", h => _showHelp = h != null }
                };
        }

        private static string[][] ParseGroups(string groupsString)
        {
            return groupsString
                .Split(';')
                .Select(g => g.Split(','))
                .ToArray();
        }

        private static string[] ParseCsv(string commaSeparatedValues)
        {
            return commaSeparatedValues.Split(',');
        }

        private static void WriteProgramDescription(TextWriter output)
        {
            output.WriteLine("FasterTests-Run v.{0}", Assembly.GetExecutingAssembly().GetName().Version);
            output.WriteLine("Copyright (c) 2013-2014 Andriy Kozachuk");
        }

        private void WriteHelp(OptionSet options, TextWriter output)
        {
            output.WriteLine();
            output.WriteLine("Usage: FasterTests-Run [testassemblies] [options]");
            output.WriteLine();
            output.WriteLine("Runs unit tests in parallel");
            output.WriteLine();
            output.WriteLine("Example: FasterTests-Run MyTestAssembly.dll -ExcludeCategories Slow");
            output.WriteLine();
            output.WriteLine("Options:");

            options.WriteOptionDescriptions(output);
        }
    }
}