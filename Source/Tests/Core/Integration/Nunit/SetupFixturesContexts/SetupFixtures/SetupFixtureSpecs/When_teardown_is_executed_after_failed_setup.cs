using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly.FailingNamespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_teardown_is_executed_after_failed_setup : NunitSetupFixtureSpecification<SetupFixtureWhichThrowsAnException>
    {
        Because of = () =>
        {
            SetupFixtureWhichThrowsAnException.TeardownWasInvoked = false;
            Subject.Setup(An<IObserver<TestResult>>());
            Subject.Teardown(An<IObserver<TestResult>>());
        };

        It should_not_invoke_underlying_nunit_fixture = () => SetupFixtureWhichThrowsAnException.TeardownWasInvoked.ShouldBeFalse();

        It should_move_to_no_setup_state = () => Subject.State.ShouldEqual(SetupFixtureState.NoSetupExecuted);
    }
}