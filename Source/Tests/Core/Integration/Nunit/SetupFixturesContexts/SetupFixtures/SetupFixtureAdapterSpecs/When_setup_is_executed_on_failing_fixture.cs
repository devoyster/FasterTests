using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Tests.NunitTestAssembly.FailingNamespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureAdapterSpecs
{
    [Subject(typeof(SetupFixtureAdapter))]
    public class When_setup_is_executed_on_failing_fixture : SetupFixtureAdapterSpecification<SetupFixtureWhichThrowsAnException>
    {
        Because of = () =>
            result = Subject.Setup();

        It should_fail = () => result.ShouldBeFalse();

        private static bool result;
    }
}