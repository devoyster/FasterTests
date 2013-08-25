using System;
using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Interfaces.Models;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_test_engine_is_disposed : TestEngineSpecification
    {
        Because of = () =>
            Subject.Dispose();

        It should_teardown_all_remaining_setup_fixtures = () => The<ISetupFixturesContext>().WasToldTo(c => c.TeardownAll(Param.IsAny<IObserver<TestResult>>())).OnlyOnce();
    }
}