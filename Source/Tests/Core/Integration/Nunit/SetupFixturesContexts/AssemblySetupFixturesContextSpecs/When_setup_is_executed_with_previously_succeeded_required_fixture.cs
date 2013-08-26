using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed_with_previously_succeeded_required_fixture : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            succeededFixture = CreateFixture(isRequired: true, isSetupSucceeded: true);
            requiredFixture = CreateFixture(isRequired: true);

            ConfigureTreeBuilder(
                new Tree<ISetupFixture>(RootFixture)
                    {
                        new Tree<ISetupFixture>(succeededFixture)
                            {
                                requiredFixture
                            }
                    });
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_succeed = () => result.ShouldBeTrue();

        It should_skip_already_succeeded_fixture = () => succeededFixture.WasNotToldTo(f => f.Setup(TheResultsObserver));

        It should_setup_newly_required_fixture = () => requiredFixture.WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        private static bool result;
        private static ISetupFixture succeededFixture;
        private static ISetupFixture requiredFixture;
    }
}