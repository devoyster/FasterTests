using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_teardown_is_executed_after_setup : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            setupFixture = CreateFixture(isSetupSucceeded: true);
            notSetupFixture = CreateFixture();

            ConfigureTreeBuilder(
                new Tree<ISetupFixture>(RootFixture)
                    {
                        setupFixture,
                        notSetupFixture
                    });
        };

        Because of = () =>
            Subject.TeardownAll(TheResultsObserver);

        It should_teardown_setup_fixture = () => setupFixture.WasToldTo(f => f.Teardown(TheResultsObserver)).OnlyOnce();

        It should_skip_not_setup_fixture = () => notSetupFixture.WasNotToldTo(f => f.Teardown(TheResultsObserver));

        private static ISetupFixture setupFixture;
        private static ISetupFixture notSetupFixture;
    }
}