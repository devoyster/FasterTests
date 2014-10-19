using System;
using System.IO;
using System.Reflection;
using FasterTests.Core;

namespace FasterTests.ConsoleRunner
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            WriteDescription(Console.Out);
            new TestRunnerCommand(new TestRunner()).Execute(args, Console.Out);
        }

        private static void WriteDescription(TextWriter output)
        {
            output.WriteLine("FasterTests-Run v.{0}", Assembly.GetExecutingAssembly().GetName().Version);
            output.WriteLine("Copyright (c) 2013-2014 Andriy Kozachuk");
            output.WriteLine();
        }
    }
}