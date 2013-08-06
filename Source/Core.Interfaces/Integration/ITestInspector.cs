using System.Collections.Generic;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Interfaces.Integration
{
    public interface ITestInspector
    {
        IEnumerable<TestDescriptor> LoadAllTestsFrom(string assemblyPath);
    }
}