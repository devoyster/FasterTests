using System.Collections.Generic;
using Funt.Core.Models;

namespace Funt.Core
{
    public interface ITestResultsWriter
    {
        void Write(IEnumerable<TestResult> results);
    }
}