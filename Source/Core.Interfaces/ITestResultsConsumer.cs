using System.Collections.Generic;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Interfaces
{
    public interface ITestResultsConsumer
    {
        void Consume(IEnumerable<TestResult> results);
    }
}