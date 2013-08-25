using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_for_two_fixtures_from_the_same_namespace : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            ConfigureFixtureFor<NamespaceSetupFixture>(isRequired: true);
            ConfigureFixtureFor<SameNamespaceSetupFixture>(isRequired: true);
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_succeed = () => result.ShouldBeTrue();

        It should_setup_first_fixture = () => TheFixtureFor<NamespaceSetupFixture>().WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        It should_skip_second_fixture = () => TheFixtureFor<SameNamespaceSetupFixture>().WasNotToldTo(f => f.Setup(TheResultsObserver));

        private static bool result;
    }
}