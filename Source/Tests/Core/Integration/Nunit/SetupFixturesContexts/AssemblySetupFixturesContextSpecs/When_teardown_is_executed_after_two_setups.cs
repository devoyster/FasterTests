using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_teardown_is_executed_after_two_setups : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            firstFixture = CreateFixture(isSetupSucceeded: true);
            secondFixture = CreateFixture(isSetupFailed: true);

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
                .WhenToldTo(f => f.Teardown(TheResultsObserver))
                .Callback(() => firstFixtureOrder = order++);

            secondFixture
                .WhenToldTo(f => f.Teardown(TheResultsObserver))
                .Callback(() => secondFixtureOrder = order++);
        };

        Because of = () =>
            Subject.TeardownAll(TheResultsObserver);

        It should_teardown_first_fixture = () => firstFixture.WasToldTo(f => f.Teardown(TheResultsObserver)).OnlyOnce();

        It should_teardown_second_fixture = () => secondFixture.WasToldTo(f => f.Teardown(TheResultsObserver)).OnlyOnce();

        It should_teardown_second_fixture_first = () => secondFixtureOrder.ShouldEqual(1);

        It should_teardown_first_fixture_next = () => firstFixtureOrder.ShouldEqual(2);

        private static ISetupFixture firstFixture;
        private static ISetupFixture secondFixture;

        private static int order;
        private static int firstFixtureOrder;
        private static int secondFixtureOrder;
    }
}