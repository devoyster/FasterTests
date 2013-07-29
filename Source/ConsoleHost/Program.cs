using System;
using System.Reflection;
using Funt.Core.Nunit;
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
            var tests = inspector.FindAllTests(assembly);

            tests
                .Select(d => d.Name)
                .ForEach(Console.WriteLine);
        }
    }
}