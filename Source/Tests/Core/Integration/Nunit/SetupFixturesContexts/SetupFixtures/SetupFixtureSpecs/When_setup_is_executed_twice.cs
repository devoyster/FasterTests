using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly.AnotherNamespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_setup_is_executed_twice : NunitSetupFixtureSpecification<AnotherNamespaceSetupFixture>
    {
        private Because of = () =>
            exception = Catch.Exception(() =>
            {
                Subject.Setup(An<IObserver<TestResult>>());
                Subject.Setup(An<IObserver<TestResult>>());
            });

        It should_fail = () => exception.ShouldBeOfType<InvalidOperationException>();

        It should_not_change_state = () => Subject.State.ShouldEqual(SetupFixtureState.SetupSucceeded);

        private static Exception exception;
    }
}