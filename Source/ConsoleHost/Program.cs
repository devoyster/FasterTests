﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Funt.Core;
using Funt.Core.Nunit;
using Funt.Core.Workers;
using Funt.Helpers;
using System.Linq;

namespace Funt.ConsoleHost
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var inspector = new NunitTestInspector();
            var tests = inspector
                            .FindAllTests(args[0])
                            .OrderBy(d => d.Name)
                            .ToList();

            tests
                .Select(t => t.Name)
                .ForEach(Console.WriteLine);

            Console.WriteLine();
            Console.WriteLine("count = {0}", tests.Count);
            Console.WriteLine();

            Console.WriteLine("Run results:");

            var sw = Stopwatch.StartNew();

            var dispatcher = new TestDispatcher(new TestWorkersPool());
            var results = dispatcher.RunTestsAsync(tests);

            var errors = new List<string>();
            int count = 0;
            results.ForEach(r =>
                                {
                                    count++;
                                    if (r.IsSuccess)
                                    {
                                        Console.Write('.');
                                    }
                                    else if (r.IsIgnored)
                                    {
                                        Console.Write('I');
                                    }
                                    else
                                    {
                                        Console.Write('F');
                                        errors.Add(r.ErrorMessage);
                                    }
                                });

            sw.Stop();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("time = {0}s", sw.Elapsed.TotalSeconds);
            Console.WriteLine("count = {0}", count);
            Console.WriteLine();
            Console.WriteLine("failed = {0}", errors.Count);
            Console.WriteLine();

            errors.ForEach(Console.WriteLine);
        }
    }
}