using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_with_previously_failed_required_fixture : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            ConfigureFixtureFor<RootSetupFixture>(isRequired: true, isSetupFailed: true);
            ConfigureFixtureFor<NamespaceSetupFixture>(isRequired: true);
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_fail = () => result.ShouldBeFalse();

        It should_skip_already_failed_fixture = () => TheFixtureFor<RootSetupFixture>().WasNotToldTo(f => f.Setup(TheResultsObserver));

        It should_set_parent_failed_for_newly_required_fixture = () => TheFixtureFor<NamespaceSetupFixture>().WasToldTo(f => f.SetParentFailed(TheResultsObserver)).OnlyOnce();

        private static bool result;
    }
}