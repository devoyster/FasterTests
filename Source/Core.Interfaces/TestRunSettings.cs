using System.IO;

namespace FasterTests.Core.Interfaces
{
    public class TestRunSettings
    {
        public string AssemblyPath { get; set; }
        public string[] IncludeCategories { get; set; }
        public string[] ExcludeCategories { get; set; }
        public int DegreeOfParallelism { get; set; }
        public string[][] NoParallelGroups { get; set; }
        public string[] ConfigStringsToPatch { get; set; }
        public TextWriter Output { get; set; }
    }
}