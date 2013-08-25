using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_parent_fixture_failed : SetupFixtureSpecification<RootSetupFixture>
    {
        Establish context = () =>
            RootSetupFixture.SetupWasInvoked = false;

        Because of = () =>
            Subject.SetParentFailed(An<IObserver<TestResult>>());

        It should_not_invoke_underlying_nunit_fixture = () => RootSetupFixture.SetupWasInvoked.ShouldBeFalse();

        It should_move_to_setup_failed_state = () => Subject.State.ShouldEqual(SetupFixtureState.SetupFailed);
    }
}