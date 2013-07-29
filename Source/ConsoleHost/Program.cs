using System;
using System.Reflection;
using Funt.Core;
using Funt.Core.Nunit;
using Funt.Core.Workers;
using Funt.Helpers;
using System.Linq;
using NUnit.Core;

namespace Funt.ConsoleHost
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.LoadFrom(args[0]);

            CoreExtensions.Host.InitializeService();

            var inspector = new NunitTestInspector();
            var tests = inspector.FindAllTests(assembly).ToList();

            tests
                .Select(t => t.Name)
                .ForEach(Console.WriteLine);

            Console.WriteLine();
            Console.WriteLine("count = {0}", tests.Count);
            Console.WriteLine();

            Console.WriteLine("Run results:");

            var dispatcher = new TestDispatcher(new TestWorkersPool());
            var results = dispatcher.RunTestsAsync(tests);

            int count = 0;
            results.ForEach(r =>
                                {
                                    count++;
                                    Console.Write(r.IsSuccess ? '.' : 'F');
                                });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("count = {0}", count);
        }
    }
}