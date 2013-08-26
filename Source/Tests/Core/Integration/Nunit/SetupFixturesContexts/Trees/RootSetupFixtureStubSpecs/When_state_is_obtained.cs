using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.RootSetupFixtureStubSpecs
{
    [Subject(typeof(RootSetupFixtureStub))]
    public class When_state_is_obtained : WithSubject<RootSetupFixtureStub>
    {
        Because of = () =>
            state = Subject.State;

        It should_always_be_setup_succceeded = () => state.ShouldEqual(SetupFixtureState.SetupSucceeded);

        private static SetupFixtureState state;
    }
}