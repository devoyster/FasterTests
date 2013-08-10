using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Core.Interfaces.Models;
using Machine.Specifications;
using System.Reactive.Linq;
using Machine.Specifications.Annotations;

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
            var test = new TestDescriptor
                           {
                               Name = typeof(T).FullName,
                               AssemblyPath = typeof(T).Assembly.Location
                           };

            var results = subject.RunTests(new[] { test });

            TestResult = results.Single();
        }
    }
}