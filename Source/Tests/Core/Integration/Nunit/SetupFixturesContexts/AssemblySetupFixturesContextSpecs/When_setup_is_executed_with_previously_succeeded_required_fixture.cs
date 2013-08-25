using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_with_previously_succeeded_required_fixture : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            ConfigureFixtureFor<RootSetupFixture>(isRequired: true, isSetupSucceeded: true);
            ConfigureFixtureFor<NamespaceSetupFixture>(isRequired: true);
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_succeed = () => result.ShouldBeTrue();

        It should_skip_already_succeeded_fixture = () => TheFixtureFor<RootSetupFixture>().WasNotToldTo(f => f.Setup(TheResultsObserver));

        It should_setup_newly_required_fixture = () => TheFixtureFor<NamespaceSetupFixture>().WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        private static bool result;
    }
}