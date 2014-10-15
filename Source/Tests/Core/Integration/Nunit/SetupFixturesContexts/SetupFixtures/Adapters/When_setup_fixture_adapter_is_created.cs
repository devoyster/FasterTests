using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters
{
    [Subject(typeof(SetupFixtureAdapterFactory))]
    public class When_setup_fixture_adapter_is_created : WithSubject<SetupFixtureAdapterFactory>
    {
        Because of = () =>
            adapter = Subject.Create(typeof(RootSetupFixture));

        It should_create_instance_of_setup_fixture_adapter = () => adapter.ShouldBeOfExactType<SetupFixtureAdapter>();

        private static ISetupFixtureAdapter adapter;
    }
}