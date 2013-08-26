using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_with_not_required_fixture : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            notRequiredFixture = CreateFixture(isSetupSucceeded: true);
            requiredFixture = CreateFixture(isRequired: true);

            ConfigureTreeBuilder(
                new Tree<ISetupFixture>(RootFixture)
                    {
                        notRequiredFixture,
                        requiredFixture
                    });
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_succeed = () => result.ShouldBeTrue();

        It should_teardown_not_required_fixture = () => notRequiredFixture.WasToldTo(f => f.Teardown(TheResultsObserver)).OnlyOnce();

        It should_setup_newly_required_fixture = () => requiredFixture.WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        private static bool result;
        private static ISetupFixture notRequiredFixture;
        private static ISetupFixture requiredFixture;
    }
}