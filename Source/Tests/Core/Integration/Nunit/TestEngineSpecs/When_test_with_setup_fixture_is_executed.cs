using System;
using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_test_with_setup_fixture_is_executed : NunitTestEngineSpecification
    {
        Because of = () =>
            RunTest<TestWithSetupFixture>();

        It should_invoke_setup_fixture = () => The<ISetupFixturesContext>().WasToldTo(c => c.SetupFor(Param.IsAny<TestDescriptor>(), Param.IsAny<IObserver<TestResult>>())).OnlyOnce();
    }
}