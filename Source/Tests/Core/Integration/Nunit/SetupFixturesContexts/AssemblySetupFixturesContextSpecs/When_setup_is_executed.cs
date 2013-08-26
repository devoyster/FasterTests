using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_executed : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            requiredFixture = CreateFixture(isRequired: true);
            notRequiredFixture = CreateFixture();

            ConfigureTreeBuilder(
                new Tree<ISetupFixture>(RootFixture)
                    {
                        requiredFixture,
                        notRequiredFixture
                    });
        };

        Because of = () =>
            result = Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_succeed = () => result.ShouldBeTrue();

        It should_setup_required_fixture = () => requiredFixture.WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        It should_skip_not_required_fixture = () => notRequiredFixture.WasNotToldTo(f => f.Setup(TheResultsObserver));

        private static bool result;
        private static ISetupFixture requiredFixture;
        private static ISetupFixture notRequiredFixture;
    }
}