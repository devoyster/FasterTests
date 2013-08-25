using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_teardown_is_executed_after_two_setups : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            order = 1;

            ConfigureFixtureFor<RootSetupFixture>(isSetupSucceeded: true)
                .WhenToldTo(f => f.Teardown(TheResultsObserver))
                .Callback(() => firstFixtureOrder = order++);

            ConfigureFixtureFor<NamespaceSetupFixture>(isSetupFailed: true)
                .WhenToldTo(f => f.Teardown(TheResultsObserver))
                .Callback(() => secondFixtureOrder = order++);
        };

        Because of = () =>
            Subject.TeardownAll(TheResultsObserver);

        It should_teardown_first_fixture = () => TheFixtureFor<RootSetupFixture>().WasToldTo(f => f.Teardown(TheResultsObserver)).OnlyOnce();

        It should_teardown_second_fixture = () => TheFixtureFor<NamespaceSetupFixture>().WasToldTo(f => f.Teardown(TheResultsObserver)).OnlyOnce();

        It should_teardown_second_fixture_first = () => secondFixtureOrder.ShouldEqual(1);

        It should_teardown_first_fixture_next = () => firstFixtureOrder.ShouldEqual(2);

        private static int order;
        private static int firstFixtureOrder;
        private static int secondFixtureOrder;
    }
}