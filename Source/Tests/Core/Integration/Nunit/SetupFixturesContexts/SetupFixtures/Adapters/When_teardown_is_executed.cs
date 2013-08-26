using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters
{
    [Subject(typeof(SetupFixtureAdapter))]
    public class When_teardown_is_executed : SetupFixtureAdapterSpecification<RootSetupFixture>
    {
        Establish context = () =>
        {
            RootSetupFixture.TeardownWasInvoked = false;
            Subject.Setup();
        };

        Because of = () =>
            Subject.Teardown();

        It should_invoke_underlying_nunit_fixture = () => RootSetupFixture.TeardownWasInvoked.ShouldBeTrue();
    }
}