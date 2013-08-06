using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using FasterTests.Core.Models;
using FasterTests.Core.Workers;

namespace FasterTests.Core.Implementation
{
    public class TestDispatcher : ITestDispatcher
    {
        private readonly ITestWorkersPool _testWorkersPool;
        private readonly string[][] _noParallelGroups;

        public TestDispatcher(ITestWorkersPool testWorkersPool, string[][] noParallelGroups)
        {
            _testWorkersPool = testWorkersPool;
            _noParallelGroups = noParallelGroups;
        }

        public IEnumerable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests)
        {
            var batches = SplitTests(tests);

            Environment.CurrentDirectory = Path.GetDirectoryName(tests.First().AssemblyPath);

            var observables = batches.Select(_testWorkersPool.RunTestsAsync);

            return observables
                    .Merge()
                    .ToEnumerable();
        }

        private IEnumerable<IEnumerable<TestDescriptor>> SplitTests(IEnumerable<TestDescriptor> tests)
        {
            var groups = _noParallelGroups;

            var grouped = tests
                            .Select(d => new
                                             {
                                                 d,
                                                 group = groups != null
                                                            ? groups.FirstOrDefault(g => g.Any(ns => d.Name.StartsWith(ns))).First()
                                                            : d.Name.Substring(0, d.Name.LastIndexOf('.'))
                                             })
                            .GroupBy(x => x.group)
                            .Select(g => g.Select(x => x.d).ToList());
            var groupedQueue = new Queue<List<TestDescriptor>>(grouped);

            var maxDegreeOfParallelism = Environment.ProcessorCount;

            var batchSize = tests.Count() / maxDegreeOfParallelism + 1;
            var buckets = Enumerable
                            .Repeat(1, maxDegreeOfParallelism)
                            .Select(_ => new List<TestDescriptor>(batchSize))
                            .ToArray();

            for (int i = 0; i < buckets.Length - 1; i++)
            {
                var bucket = buckets[i];

                while (groupedQueue.Count > 0 && bucket.Count < batchSize)
                {
                    bucket.AddRange(groupedQueue.Dequeue());
                }

                batchSize = groupedQueue.Sum(g => g.Count) / (maxDegreeOfParallelism - i - 1) + 1;
            }

            buckets[buckets.Length - 1].AddRange(groupedQueue.SelectMany(g => g));

            return buckets;
        }
    }
}