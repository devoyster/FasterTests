using System.Collections.Generic;
using Funt.Core.Models;

namespace Funt.Core.Integration
{
    public interface ITestInspector
    {
        IEnumerable<TestDescriptor> FindAllTests(string assemblyPath);
    }
}