using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_teardown_is_executed_after_setup : SetupFixtureSpecification<RootSetupFixture>
    {
        Establish context = () =>
            Subject.Setup(An<IObserver<TestResult>>());

        Because of = () =>
            Subject.Teardown(An<IObserver<TestResult>>());

        It should_invoke_underlying_nunit_fixture = () => The<ISetupFixtureAdapter>().WasToldTo(a => a.Teardown()).OnlyOnce();

        It should_move_to_no_setup_state = () => Subject.IsExecuted.ShouldBeFalse();

        It should_create_adapter = () => The<ISetupFixtureAdapterFactory>().WasToldTo(f => f.Create(typeof(RootSetupFixture))).OnlyOnce();
    }
}