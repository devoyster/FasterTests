using System;

namespace FasterTests.Core.Interfaces.Models
{
    [Serializable]
    public class TestDescriptor
    {
        public string Name { get; set; }
        public string AssemblyPath { get; set; }
    }
}