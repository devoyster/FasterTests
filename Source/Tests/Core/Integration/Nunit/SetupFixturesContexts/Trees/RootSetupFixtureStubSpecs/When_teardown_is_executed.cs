using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.RootSetupFixtureStubSpecs
{
    [Subject(typeof(RootSetupFixtureStub))]
    public class When_teardown_is_executed : WithSubject<RootSetupFixtureStub>
    {
        Because of = () =>
            Subject.Teardown(null);

        It should_succeed = () => {};

        It should_not_change_state = () => Subject.State.ShouldEqual(SetupFixtureState.SetupSucceeded);
    }
}