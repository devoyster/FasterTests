using System;
using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Interfaces.Integration;
using FasterTests.Core.Interfaces.Models;
using Machine.Fakes;
using Machine.Specifications;
using FasterTests.Tests.TestHelpers;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    public abstract class TestEngineSpecification : WithSubject<TestEngine>
    {
        Establish context = () =>
        {
            Configure<ITestFrameworkInitializer, TestFrameworkInitializer>();
            WhenToldToSetupForReturn(true);

            TestResult = null;
        };

        Cleanup after = () =>
            Subject.Dispose();

        protected static TestResult TestResult { get; private set; }

        protected static void RunTest<T>() where T : class
        {
            var test = typeof(T).GetTestDescriptor();

            Subject.Results.Subscribe(r => TestResult = r);

            Subject.RunTest(test);
        }

        protected static void WhenToldToSetupForReturn(bool result)
        {
            The<ISetupFixturesContext>()
                .WhenToldTo(c => c.SetupFor(Param.IsAny<TestDescriptor>(), Param.IsAny<IObserver<TestResult>>()))
                .Return(result);
        }
    }
}