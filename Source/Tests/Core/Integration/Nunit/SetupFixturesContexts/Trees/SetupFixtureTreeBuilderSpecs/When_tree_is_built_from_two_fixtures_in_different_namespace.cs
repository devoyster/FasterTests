using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using FasterTests.Tests.NunitTestAssembly.AnotherNamespace;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using FasterTests.Helpers.Trees;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.SetupFixtureTreeBuilderSpecs
{
    [Subject(typeof(SetupFixtureTreeBuilder))]
    public class When_tree_is_built_from_two_fixtures_in_different_namespace : SetupFixtureTreeBuilderSpecification
    {
        private Establish context = () =>
        {
            firstFixture = AddFixtureFor<NamespaceSetupFixture>();
            secondFixture = AddFixtureFor<AnotherNamespaceSetupFixture>();
        };

        Because of = () =>
            Tree = Subject.BuildFrom(AssemblyPath);

        It should_contain_both_fixtures = () => Tree.Children.Values().ShouldContainOnly(firstFixture, secondFixture);

        private static ISetupFixture firstFixture;
        private static ISetupFixture secondFixture;
    }
}