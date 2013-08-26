using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_parent_fixture_failed : SetupFixtureSpecification<RootSetupFixture>
    {
        Because of = () =>
            Subject.SetParentFailed(An<IObserver<TestResult>>());

        It should_not_invoke_underlying_nunit_fixture = () => The<ISetupFixtureAdapter>().WasNotToldTo(a => a.Setup());

        It should_move_to_setup_failed_state = () => Subject.State.ShouldEqual(SetupFixtureState.SetupFailed);

        It should_not_create_adapter = () => The<ISetupFixtureAdapterFactory>().WasNotToldTo(f => f.Create(typeof(RootSetupFixture)));
    }
}