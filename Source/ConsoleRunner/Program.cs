using System.Linq;
using FasterTests.Core;
using FasterTests.Core.Models;

namespace FasterTests.ConsoleRunner
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            string[][] noParallelGroups = null;
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                noParallelGroups = string.IsNullOrEmpty(args[1])
                                    ? null
                                    : args[1].Split(';').Select(g => g.Split(',').ToArray()).ToArray();
            }

            string[] configStringsToPatch = null;
            if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            {
                configStringsToPatch = args[2].Split(',');
            }

            new TestRunner().Run(new TestRunSettings
                                     {
                                         AssemblyPath = args[0],
                                         NoParallelGroups = noParallelGroups,
                                         ConfigStringsToPatch = configStringsToPatch
                                     });
        }
    }
}