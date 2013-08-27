using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.RootSetupFixtureStubSpecs
{
    [Subject(typeof(RootSetupFixtureStub))]
    public class When_state_is_obtained : WithSubject<RootSetupFixtureStub>
    {
        It should_always_be_executed = () => Subject.IsExecuted.ShouldBeTrue();

        It should_always_be_succceeded = () => Subject.IsSucceeded.ShouldBeTrue();

        It should_never_be_failed = () => Subject.IsFailed.ShouldBeFalse();
    }
}