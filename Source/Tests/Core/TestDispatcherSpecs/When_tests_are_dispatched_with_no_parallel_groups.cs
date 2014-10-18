using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using FasterTests.Core;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Core.Interfaces.Workers;
using FasterTests.Helpers.Collections;
using FasterTests.Tests.TestHelpers;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.TestDispatcherSpecs
{
    [Subject(typeof(TestDispatcher))]
    public class When_tests_are_dispatched_with_no_parallel_groups : WithFakes
    {
        Establish context = () =>
        {
            var noParallelGroups = new[]
                                       {
                                           new[] { "Namespace1.", "Namespace2." },
                                           new[] { "Namespace3." }
                                       };
            subject = new TestDispatcher(The<ITestWorkersPool>(), new TestRunSettings { NoParallelGroups = noParallelGroups });

            tests = new[]
                        {
                            new TestDescriptor { Name = "Namespace1.Test1" },
                            new TestDescriptor { Name = "Namespace2.Test1" },
                            new TestDescriptor { Name = "Namespace2.Test2" },
                            new TestDescriptor { Name = "Namespace3.Test1" },
                            new TestDescriptor { Name = "Namespace3.Test2" },
                            new TestDescriptor { Name = "Namespace3.Test3" },
                            new TestDescriptor { Name = "Namespace3.Test4" }
                        };
            tests.ForEach(d => d.AssemblyPath = Assembly.GetExecutingAssembly().Location);

            buckets = new List<List<TestDescriptor>>();
            The<ITestWorkersPool>()
                .WhenToldTo(p => p.RunTests(Param.IsAny<IEnumerable<TestDescriptor>>()))
                .Return((IEnumerable<TestDescriptor> t) =>
                            {
                                buckets.Add(t.ToList());
                                return Observable.Empty<TestResult>();
                            }); 
        };

        Because of = () =>
            subject.RunTests(tests).ConsumeAll();

        It should_group_together_tests_from_the_first_no_parallel_group = () => buckets[0].Count.ShouldEqual(3);

        It should_group_together_tests_from_the_second_no_parallel_group = () => buckets[1].Count.ShouldEqual(4);

        private static TestDispatcher subject;
        private static IEnumerable<TestDescriptor> tests;
        private static List<List<TestDescriptor>> buckets;
    }
}