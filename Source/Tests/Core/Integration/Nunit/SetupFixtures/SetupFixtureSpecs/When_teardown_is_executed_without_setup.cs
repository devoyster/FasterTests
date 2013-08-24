using System;
using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Integration.Nunit.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_teardown_is_executed_without_setup : NunitSetupFixtureSpecification<RootSetupFixture>
    {
        Because of = () =>
            exception = Catch.Exception(() => Subject.Teardown(An<IObserver<TestResult>>()));

        It should_fail = () => exception.ShouldBeOfType<InvalidOperationException>();

        It should_not_change_state = () => Subject.State.ShouldEqual(SetupFixtureState.NoSetupExecuted);

        private static Exception exception;
    }
}