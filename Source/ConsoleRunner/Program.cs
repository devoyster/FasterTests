using System;
using FasterTests.Core;

namespace FasterTests.ConsoleRunner
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            new TestRunnerCommand(new TestRunner()).Execute(args, Console.Out);
        }
    }
}