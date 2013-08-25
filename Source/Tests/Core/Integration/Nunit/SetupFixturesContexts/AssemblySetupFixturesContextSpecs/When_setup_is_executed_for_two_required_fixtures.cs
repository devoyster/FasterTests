using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_for_two_required_fixtures : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            order = 1;

            ConfigureFixtureFor<RootSetupFixture>(isRequired: true)
                .WhenToldTo(f => f.Setup(TheResultsObserver))
                .Callback(() =>
                              {
                                  firstFixtureOrder = order++;
                                  SetFixtureState<RootSetupFixture>(SetupFixtureState.SetupSucceeded);
                              });

            ConfigureFixtureFor<NamespaceSetupFixture>(isRequired: true)
                .WhenToldTo(f => f.Setup(TheResultsObserver))
                .Callback(() =>
                              {
                                  secondFixtureOrder = order++;
                                  SetFixtureState<NamespaceSetupFixture>(SetupFixtureState.SetupSucceeded);
                              });
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_succeed = () => result.ShouldBeTrue();

        It should_setup_first_fixture = () => TheFixtureFor<RootSetupFixture>().WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        It should_setup_second_fixture = () => TheFixtureFor<NamespaceSetupFixture>().WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        It should_setup_first_fixture_first = () => firstFixtureOrder.ShouldEqual(1);

        It should_setup_second_fixture_next = () => secondFixtureOrder.ShouldEqual(2);

        private static bool result;
        private static int order;
        private static int firstFixtureOrder;
        private static int secondFixtureOrder;
    }
}