using Funt.Core;
using System.Linq;

namespace Funt.ConsoleHost
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

            var runner = new TestRunner(args[0], noParallelGroups, configStringsToPatch);
            runner.Run();
        }
    }
}