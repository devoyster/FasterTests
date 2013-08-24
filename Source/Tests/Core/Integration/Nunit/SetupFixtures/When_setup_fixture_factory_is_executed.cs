using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Integration.Nunit.SetupFixtures;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixtures
{
    [Subject(typeof(SetupFixtureFactory))]
    public class When_setup_fixture_is_created : WithSubject<SetupFixtureFactory>
    {
        Because of = () =>
            setupFixture = Subject.Create(typeof(RootSetupFixture));

        It should_create_instance_of_setup_fixture = () => setupFixture.ShouldBeOfType<SetupFixture>();

        It should_initialize_fixture_with_supplied_type = () => setupFixture.Type.ShouldEqual(typeof(RootSetupFixture));

        private static ISetupFixture setupFixture;
    }
}