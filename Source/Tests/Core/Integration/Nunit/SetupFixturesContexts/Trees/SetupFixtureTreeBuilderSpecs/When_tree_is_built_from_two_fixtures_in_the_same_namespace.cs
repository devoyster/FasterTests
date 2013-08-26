using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using FasterTests.Helpers.Trees;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.SetupFixtureTreeBuilderSpecs
{
    [Subject(typeof(SetupFixtureTreeBuilder))]
    public class When_tree_is_built_from_two_fixtures_in_the_same_namespace : SetupFixtureTreeBuilderSpecification
    {
        private Establish context = () =>
        {
            firstFixture = AddFixtureFor<NamespaceSetupFixture>();
            secondFixture = AddFixtureFor<SameNamespaceSetupFixture>();
        };

        Because of = () =>
            Tree = Subject.BuildFrom(AssemblyPath);

        It should_contain_first_fixture = () => Tree.Children.Values().ShouldContain(firstFixture);

        It should_not_contain_second_fixture = () => Tree.Children.Values().ShouldNotContain(secondFixture);

        private static ISetupFixture firstFixture;
        private static ISetupFixture secondFixture;
    }
}