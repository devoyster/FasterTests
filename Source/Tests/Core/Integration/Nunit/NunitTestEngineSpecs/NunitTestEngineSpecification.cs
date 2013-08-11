using System;
using System.Collections.Generic;
using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Core.Interfaces.Models;
using Machine.Specifications;
using System.Reactive.Linq;
using Machine.Specifications.Annotations;
using System.Linq;

namespace FasterTests.Tests.Core.Integration.Nunit.NunitTestEngineSpecs
{
    public abstract class NunitTestEngineSpecification
    {
        [UsedImplicitly]
        Establish context = () =>
        {
            subject = new NunitTestEngine();
        };

        private static NunitTestEngine subject;

        protected static TestResult TestResult { get; private set; }

        protected static void RunTest<T>() where T : class
        {
            TestResult = RunTests(typeof(T)).Single();
        }

        protected static IList<TestResult> RunTests(params Type[] testTypes)
        {
            var tests = testTypes.Select(t => new TestDescriptor
                                                  {
                                                      Name = t.FullName,
                                                      AssemblyPath = t.Assembly.Location
                                                  });

            var results = subject.RunTests(tests);

            return results.ToEnumerable().ToList();
        }
    }
}