using System.Collections.Generic;
using FasterTests.Core.Models;

namespace FasterTests.Core.Integration
{
    public interface ITestInspector
    {
        IEnumerable<TestDescriptor> FindAllTests(string assemblyPath);
    }
}