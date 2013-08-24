using System;
using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Integration.Nunit.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly.FailingNamespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_teardown_is_executed_on_failing_fixture : NunitSetupFixtureSpecification<SetupFixtureWhichThrowsAnExceptionInTeardown>
    {
        Because of = () =>
        {
            Subject.Setup(An<IObserver<TestResult>>());
            Subject.Teardown(An<IObserver<TestResult>>());
        };

        It should_move_to_no_setup_state = () => Subject.State.ShouldEqual(SetupFixtureState.NoSetupExecuted);
    }
}