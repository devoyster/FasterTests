using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using Funt.Core.Models;
using Funt.Core.Workers;
using System.Linq;

namespace Funt.Core
{
    public class TestDispatcher
    {
        private readonly TestWorkersPool _testWorkersPool;
        private readonly string[][] _noParallelGroups;

        public TestDispatcher(TestWorkersPool testWorkersPool, string[][] noParallelGroups)
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
            var groups = _noParallelGroups ?? new string[0][];

            var grouped = tests
                            .Select(d => new { d, group = groups.FirstOrDefault(g => g.Any(ns => d.Name.StartsWith(ns))) })
                            .GroupBy(x => x.group != null ? x.group[0] : x.d.Name)
                            .Select(g => g.Select(x => x.d).ToList())
                            .OrderByDescending(g => g.Count)
                            .ThenBy(g => g[0].Name)
                            .ToList();

            var maxDegreeOfParallelism = Environment.ProcessorCount;

            var batchSize = tests.Count() / maxDegreeOfParallelism + 1;
            var buckets = Enumerable
                            .Repeat(1, maxDegreeOfParallelism)
                            .Select(_ => new List<TestDescriptor>(batchSize))
                            .ToArray();

            for (int i = 0; i < buckets.Length - 1; i++)
            {
                var bucket = buckets[i];

                while (grouped.Any())
                {
                    var firstGroup = grouped[0];
                    grouped.RemoveAt(0);
                    bucket.AddRange(firstGroup);

                    if (bucket.Count > batchSize)
                    {
                        break;
                    }

                    while (grouped.Any() && bucket.Count + grouped[grouped.Count - 1].Count <= batchSize)
                    {
                        var lastGroup = grouped[grouped.Count - 1];
                        grouped.RemoveAt(grouped.Count - 1);
                        bucket.AddRange(lastGroup);
                    }
                }

                batchSize = grouped.Sum(g => g.Count) / (maxDegreeOfParallelism - i - 1) + 1;
            }

            buckets[buckets.Length - 1].AddRange(grouped.SelectMany(g => g));

            return buckets;
        }
    }
}