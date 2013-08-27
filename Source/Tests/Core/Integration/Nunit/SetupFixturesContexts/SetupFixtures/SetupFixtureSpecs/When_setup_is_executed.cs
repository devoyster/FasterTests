using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_setup_is_executed : SetupFixtureSpecification<RootSetupFixture>
    {
        Because of = () =>
            Subject.Setup(An<IObserver<TestResult>>());

        It should_invoke_underlying_nunit_fixture = () => The<ISetupFixtureAdapter>().WasToldTo(a => a.Setup()).OnlyOnce();

        It should_move_to_setup_succeeded_state = () => Subject.IsSucceeded.ShouldBeTrue();

        It should_be_marked_as_executed = () => Subject.IsExecuted.ShouldBeTrue();

        It should_create_adapter = () => The<ISetupFixtureAdapterFactory>().WasToldTo(f => f.Create(typeof(RootSetupFixture))).OnlyOnce();
    }
}