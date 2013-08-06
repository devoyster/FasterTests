using System.Collections.Generic;
using FasterTests.Core.Models;

namespace FasterTests.Core
{
    public interface ITestResultsConsumer
    {
        void Consume(IEnumerable<TestResult> results);
    }
}