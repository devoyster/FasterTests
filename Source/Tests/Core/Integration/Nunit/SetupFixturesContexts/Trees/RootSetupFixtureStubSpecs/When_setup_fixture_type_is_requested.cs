using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.RootSetupFixtureStubSpecs
{
    [Subject(typeof(RootSetupFixtureStub))]
    public class When_setup_fixture_type_is_requested : WithSubject<RootSetupFixtureStub>
    {
        Because of = () =>
            exception = Catch.Exception(() => type = Subject.Type);

        It should_fail = () => exception.ShouldBeOfExactType<NotSupportedException>();

        private static Type type;
        private static Exception exception;
    }
}