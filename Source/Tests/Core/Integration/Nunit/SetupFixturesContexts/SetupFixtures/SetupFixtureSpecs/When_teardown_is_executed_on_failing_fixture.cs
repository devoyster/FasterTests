using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Tests.NunitTestAssembly.AnotherFailingNamespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_teardown_is_executed_on_failing_fixture : NunitSetupFixtureSpecification<SetupFixtureWhichThrowsAnExceptionInTeardown>
    {
        Because of = () =>
        {
            Subject.Setup(An<IObserver<TestResult>>());
            Subject.Teardown(An<IObserver<TestResult>>());
        };

        It should_move_to_no_setup_state = () => Subject.State.ShouldEqual(SetupFixtureState.NoSetupExecuted);
    }
}