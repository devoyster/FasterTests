using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_setup_is_executed_on_failing_fixture : SetupFixtureSpecification<RootSetupFixture>
    {
        Establish context = () =>
            ConfigureAdapterToFail();

        Because of = () =>
            Subject.Setup(An<IObserver<TestResult>>());

        It should_move_to_setup_failed_state = () => Subject.IsFailed.ShouldBeTrue();

        It should_be_marked_as_executed = () => Subject.IsExecuted.ShouldBeTrue();
    }
}