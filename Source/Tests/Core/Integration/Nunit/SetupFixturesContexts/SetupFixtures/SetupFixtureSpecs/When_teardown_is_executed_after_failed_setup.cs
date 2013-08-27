using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_teardown_is_executed_after_failed_setup : SetupFixtureSpecification<RootSetupFixture>
    {
        Establish context = () =>
        {
            ConfigureAdapterToFail();
            Subject.Setup(An<IObserver<TestResult>>());
        };

        Because of = () =>
            Subject.Teardown(An<IObserver<TestResult>>());

        It should_not_invoke_underlying_nunit_fixture = () => The<ISetupFixtureAdapter>().WasNotToldTo(a => a.Teardown());

        It should_move_to_no_setup_state = () => Subject.IsExecuted.ShouldBeFalse();
    }
}