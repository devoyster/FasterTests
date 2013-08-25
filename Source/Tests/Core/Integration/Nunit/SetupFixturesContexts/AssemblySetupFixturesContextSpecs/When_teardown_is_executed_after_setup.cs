using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitTestAssembly.AnotherNamespace;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_teardown_is_executed_after_setup : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            ConfigureFixtureFor<NamespaceSetupFixture>(isSetupSucceeded: true);
            ConfigureFixtureFor<AnotherNamespaceSetupFixture>();
        };

        Because of = () =>
            Subject.TeardownAll(TheResultsObserver);

        It should_teardown_setup_fixture = () => TheFixtureFor<NamespaceSetupFixture>().WasToldTo(f => f.Teardown(TheResultsObserver)).OnlyOnce();

        It should_skip_not_setup_fixture = () => TheFixtureFor<AnotherNamespaceSetupFixture>().WasNotToldTo(f => f.Teardown(TheResultsObserver));
    }
}