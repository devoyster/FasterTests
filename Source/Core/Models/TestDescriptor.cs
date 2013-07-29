using System;

namespace Funt.Core.Models
{
    [Serializable]
    public class TestDescriptor
    {
        public string Name { get; set; }
        public string AssemblyPath { get; set; }
    }
}