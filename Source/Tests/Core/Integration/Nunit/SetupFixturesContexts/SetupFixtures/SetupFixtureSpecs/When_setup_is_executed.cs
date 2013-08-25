using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_setup_is_executed : SetupFixtureSpecification<RootSetupFixture>
    {
        Establish context = () =>
            RootSetupFixture.SetupWasInvoked = false;

        Because of = () =>
            Subject.Setup(An<IObserver<TestResult>>());

        It should_invoke_underlying_nunit_fixture = () => RootSetupFixture.SetupWasInvoked.ShouldBeTrue();

        It should_move_to_setup_succeeded_state = () => Subject.State.ShouldEqual(SetupFixtureState.SetupSucceeded);
    }
}