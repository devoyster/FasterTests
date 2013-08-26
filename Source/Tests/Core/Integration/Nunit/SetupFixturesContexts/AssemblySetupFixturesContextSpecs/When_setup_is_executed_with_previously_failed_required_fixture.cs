using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_with_previously_failed_required_fixture : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            failedFixture = CreateFixture(isRequired: true, isSetupFailed: true);
            requiredFixture = CreateFixture(isRequired: true);

            ConfigureTreeBuilder(
                new Tree<ISetupFixture>(RootFixture)
                    {
                        new Tree<ISetupFixture>(failedFixture)
                            {
                                requiredFixture
                            }
                    });
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_fail = () => result.ShouldBeFalse();

        It should_skip_already_failed_fixture = () => failedFixture.WasNotToldTo(f => f.Setup(TheResultsObserver));

        It should_set_parent_failed_for_newly_required_fixture = () => requiredFixture.WasToldTo(f => f.SetParentFailed(TheResultsObserver)).OnlyOnce();

        private static bool result;
        private static ISetupFixture failedFixture;
        private static ISetupFixture requiredFixture;
    }
}