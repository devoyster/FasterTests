using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitTestAssembly.AnotherNamespace;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_with_not_required_fixture : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            ConfigureFixtureFor<NamespaceSetupFixture>(isSetupSucceeded: true);
            ConfigureFixtureFor<AnotherNamespaceSetupFixture>(isRequired: true);
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_succeed = () => result.ShouldBeTrue();

        It should_teardown_not_required_fixture = () => TheFixtureFor<NamespaceSetupFixture>().WasToldTo(f => f.Teardown(TheResultsObserver)).OnlyOnce();

        It should_setup_newly_required_fixture = () => TheFixtureFor<AnotherNamespaceSetupFixture>().WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        private static bool result;
    }
}