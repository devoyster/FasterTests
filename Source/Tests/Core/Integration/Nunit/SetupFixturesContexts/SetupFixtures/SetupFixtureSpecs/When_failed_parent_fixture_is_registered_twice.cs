using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_failed_parent_fixture_is_registered_twice : SetupFixtureSpecification<RootSetupFixture>
    {
        Establish context = () =>
            Subject.SetParentFailed(An<IObserver<TestResult>>());

        Because of = () =>
            exception = Catch.Exception(() => Subject.SetParentFailed(An<IObserver<TestResult>>()));

        It should_fail = () => exception.ShouldBeOfType<InvalidOperationException>();

        It should_not_change_state = () => Subject.IsFailed.ShouldBeTrue();

        private static Exception exception;
    }
}