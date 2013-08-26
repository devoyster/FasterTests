using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_fixture_from_global_namespace_is_checked_for_any_other_fixture : SetupFixtureSpecification<GlobalSetupFixture>
    {
        Because of = () =>
            isRequired = Subject.IsRequiredFor(CreateFixtureFor<RootSetupFixture>());

        It should_be_required = () => isRequired.ShouldBeTrue();

        private static bool isRequired;
    }
}