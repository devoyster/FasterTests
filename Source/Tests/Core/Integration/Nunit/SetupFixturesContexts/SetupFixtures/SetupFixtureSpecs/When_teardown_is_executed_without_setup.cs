using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_teardown_is_executed_without_setup : SetupFixtureSpecification<RootSetupFixture>
    {
        Because of = () =>
            exception = Catch.Exception(() => Subject.Teardown(An<IObserver<TestResult>>()));

        It should_fail = () => exception.ShouldBeOfExactType<InvalidOperationException>();

        It should_not_change_state = () => Subject.IsExecuted.ShouldBeFalse();

        private static Exception exception;
    }
}