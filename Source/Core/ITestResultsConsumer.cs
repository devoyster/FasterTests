using System.Collections.Generic;
using Funt.Core.Models;

namespace Funt.Core
{
    public interface ITestResultsConsumer
    {
        void Consume(IEnumerable<TestResult> results);
    }
}