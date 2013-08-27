using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.RootSetupFixtureStubSpecs
{
    [Subject(typeof(RootSetupFixtureStub))]
    public class When_parent_fixture_failed : WithSubject<RootSetupFixtureStub>
    {
        Because of = () =>
            exception = Catch.Exception(() => Subject.SetParentFailed(null));

        It should_fail = () => exception.ShouldBeOfType<NotSupportedException>();

        It should_not_change_state = () => Subject.IsSucceeded.ShouldBeTrue();

        private static Exception exception;
    }
}