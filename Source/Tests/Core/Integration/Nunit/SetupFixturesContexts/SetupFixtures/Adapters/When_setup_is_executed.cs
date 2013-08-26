using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters
{
    [Subject(typeof(SetupFixtureAdapter))]
    public class When_setup_is_executed : SetupFixtureAdapterSpecification<RootSetupFixture>
    {
        Establish context = () =>
            RootSetupFixture.SetupWasInvoked = false;

        Because of = () =>
            result = Subject.Setup();

        It should_succeed = () => result.ShouldBeTrue();

        It should_invoke_underlying_nunit_fixture = () => RootSetupFixture.SetupWasInvoked.ShouldBeTrue();

        private static bool result;
    }
}