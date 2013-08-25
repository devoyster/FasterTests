using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using FasterTests.Tests.TestHelpers;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_checked_for_test_from_nested_namespace : NunitSetupFixtureSpecification<RootSetupFixture>
    {
        Because of = () =>
            isRequired = Subject.IsRequiredFor(typeof(TestWithSetupFixture).GetTestDescriptor());

        It should_be_required = () => isRequired.ShouldBeTrue();

        private static bool isRequired;
    }
}