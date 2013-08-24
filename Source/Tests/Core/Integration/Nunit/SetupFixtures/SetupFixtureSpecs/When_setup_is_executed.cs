using System;
using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Integration.Nunit.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_setup_is_executed : NunitSetupFixtureSpecification<RootSetupFixture>
    {
        Because of = () =>
        {
            RootSetupFixture.SetupWasInvoked = false;
            Subject.Setup(An<IObserver<TestResult>>());
        };

        It should_invoke_underlying_nunit_fixture = () => RootSetupFixture.SetupWasInvoked.ShouldBeTrue();

        It should_move_to_setup_succeeded_state = () => Subject.State.ShouldEqual(SetupFixtureState.SetupSucceeded);
    }
}