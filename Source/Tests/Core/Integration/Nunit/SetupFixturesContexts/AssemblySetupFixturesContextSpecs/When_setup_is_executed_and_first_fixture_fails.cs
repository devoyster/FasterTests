using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_and_first_fixture_fails : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            ConfigureFixtureFor<RootSetupFixture>(isRequired: true)
                .WhenToldTo(f => f.Setup(TheResultsObserver))
                .Callback(() => SetFixtureState<RootSetupFixture>(SetupFixtureState.SetupFailed));

            ConfigureFixtureFor<NamespaceSetupFixture>(isRequired: true);
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_fail = () => result.ShouldBeFalse();

        It should_setup_first_fixture = () => TheFixtureFor<RootSetupFixture>().WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        It should_set_parent_failed_for_second_fixture = () => TheFixtureFor<NamespaceSetupFixture>().WasToldTo(f => f.SetParentFailed(TheResultsObserver)).OnlyOnce();

        private static bool result;
    }
}