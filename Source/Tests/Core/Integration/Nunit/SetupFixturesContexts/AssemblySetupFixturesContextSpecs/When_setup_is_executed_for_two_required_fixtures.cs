using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_for_two_required_fixtures : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            firstFixture = CreateFixture(isRequired: true);
            secondFixture = CreateFixture(isRequired: true);

            ConfigureTreeBuilder(
                new Tree<ISetupFixture>(RootFixture)
                    {
                        new Tree<ISetupFixture>(firstFixture)
                            {
                                secondFixture
                            }
                    });

            order = 1;

            firstFixture
                .WhenToldTo(f => f.Setup(TheResultsObserver))
                .Callback(() =>
                            {
                                firstFixtureOrder = order++;
                                SetFixtureState(firstFixture, SetupFixtureState.SetupSucceeded);
                            });

            secondFixture
                .WhenToldTo(f => f.Setup(TheResultsObserver))
                .Callback(() =>
                            {
                                secondFixtureOrder = order++;
                                SetFixtureState(secondFixture, SetupFixtureState.SetupSucceeded);
                            });
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_succeed = () => result.ShouldBeTrue();

        It should_setup_first_fixture = () => firstFixture.WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        It should_setup_second_fixture = () => secondFixture.WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        It should_setup_first_fixture_first = () => firstFixtureOrder.ShouldEqual(1);

        It should_setup_second_fixture_next = () => secondFixtureOrder.ShouldEqual(2);

        private static bool result;
        private static ISetupFixture firstFixture;
        private static ISetupFixture secondFixture;

        private static int order;
        private static int firstFixtureOrder;
        private static int secondFixtureOrder;
    }
}