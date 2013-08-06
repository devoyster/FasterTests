namespace FasterTests.Core.Interfaces.Models
{
    public class TestRunSettings
    {
        public string AssemblyPath { get; set; }
        public string[][] NoParallelGroups { get; set; }
        public string[] ConfigStringsToPatch { get; set; }
    }
}