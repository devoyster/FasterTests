using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reflection;
using FasterTests.Core;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Core.Interfaces.Workers;
using FasterTests.Helpers.Collections;
using Machine.Fakes;
using Machine.Specifications;
using FasterTests.Tests.TestHelpers;
using System.Linq;

namespace FasterTests.Tests.Core.TestDispatcherSpecs
{
    public class When_tests_are_dispatched : WithSubject<TestDispatcher>
    {
        Establish context = () =>
        {
            tests = new[]
                        {
                            new TestDescriptor { Name = "Namespace1.Test1" },
                            new TestDescriptor { Name = "Namespace1.Test2" },
                            new TestDescriptor { Name = "Namespace2.Test1" }
                        };
            tests.ForEach(d => d.AssemblyPath = Assembly.GetExecutingAssembly().Location);

            buckets = new List<IEnumerable<TestDescriptor>>();
            The<ITestWorkersPool>()
                .WhenToldTo(p => p.RunTests(Param.IsAny<IEnumerable<TestDescriptor>>()))
                .Return((IEnumerable<TestDescriptor> t) =>
                            {
                                buckets.Add(t);
                                return Observable.Empty<TestResult>();
                            });
        };

        Because of = () =>
            Subject.RunTests(tests).ConsumeAll();

        It should_run_tests_in_parallel_workers = () => The<ITestWorkersPool>().WasToldTo(p => p.RunTests(Param.IsAny<IEnumerable<TestDescriptor>>()));

        It should_divide_tests_in_buckets = () => buckets.Count.ShouldBeGreaterThan(1);

        It should_group_together_tests_from_the_same_namespace = () => buckets.First().Count().ShouldEqual(2);

        private static IEnumerable<TestDescriptor> tests;
        private static List<IEnumerable<TestDescriptor>> buckets;
    }
}