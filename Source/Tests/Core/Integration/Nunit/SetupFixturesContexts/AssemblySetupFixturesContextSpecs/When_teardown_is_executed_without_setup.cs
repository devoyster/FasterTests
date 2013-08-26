using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Helpers.Trees;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_teardown_is_executed_without_setup : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
            ConfigureTreeBuilder(Tree.Root(RootFixture));

        Because of = () =>
            Subject.TeardownAll(TheResultsObserver);

        It should_succeed = () => {};
    }
}