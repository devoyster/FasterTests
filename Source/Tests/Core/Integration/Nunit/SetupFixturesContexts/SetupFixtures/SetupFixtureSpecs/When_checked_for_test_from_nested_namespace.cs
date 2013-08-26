using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using FasterTests.Tests.TestHelpers;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_checked_for_test_from_nested_namespace : SetupFixtureSpecification<RootSetupFixture>
    {
        Because of = () =>
            isRequired = Subject.IsRequiredFor(typeof(TestWithSetupFixture).GetTestDescriptor());

        It should_be_required = () => isRequired.ShouldBeTrue();

        It should_not_create_adapter = () => The<ISetupFixtureAdapterFactory>().WasNotToldTo(f => f.Create(typeof (RootSetupFixture)));

        private static bool isRequired;
    }
}