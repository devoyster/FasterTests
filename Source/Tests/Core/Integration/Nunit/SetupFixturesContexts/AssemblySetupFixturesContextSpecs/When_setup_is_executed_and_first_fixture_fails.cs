using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_and_first_fixture_fails : AssemblySetupFixturesContextSpecification
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

            firstFixture
                .WhenToldTo(f => f.Setup(TheResultsObserver))
                .Callback(() => SetFixtureFailed(firstFixture));
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_fail = () => result.ShouldBeFalse();

        It should_setup_first_fixture = () => firstFixture.WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        It should_set_parent_failed_for_second_fixture = () => secondFixture.WasToldTo(f => f.SetParentFailed(TheResultsObserver)).OnlyOnce();

        private static bool result;
        private static ISetupFixture firstFixture;
        private static ISetupFixture secondFixture;
    }
}