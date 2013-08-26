using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters;
using FasterTests.Tests.NunitTestAssembly.AnotherFailingNamespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters
{
    [Subject(typeof(SetupFixtureAdapter))]
    public class When_teardown_is_executed_on_failing_fixture : SetupFixtureAdapterSpecification<SetupFixtureWhichThrowsAnExceptionInTeardown>
    {
        Establish context = () =>
            Subject.Setup();

        Because of = () =>
            Subject.Teardown();

        It should_not_throw = () => {};
    }
}