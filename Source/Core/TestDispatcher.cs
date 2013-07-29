using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Funt.Core.Models;
using Funt.Core.Workers;
using Funt.Helpers;
using System.Linq;

namespace Funt.Core
{
    public class TestDispatcher
    {
        private readonly TestWorkersPool _testWorkersPool;

        public TestDispatcher(TestWorkersPool testWorkersPool)
        {
            _testWorkersPool = testWorkersPool;
        }

        public IEnumerable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests)
        {
            var batches = SplitTests(tests);
            var observables = batches.Select(_testWorkersPool.RunTestsAsync);

            return observables
                    .Merge()
                    .ToEnumerable();
        }

        private IEnumerable<IEnumerable<TestDescriptor>> SplitTests(IEnumerable<TestDescriptor> tests)
        {
            var maxDegreeOfParallelism = Environment.ProcessorCount;
            return tests.SplitInEqualBatches(maxDegreeOfParallelism);
        }
    }
}