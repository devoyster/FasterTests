using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.AnotherNamespace;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using FasterTests.Helpers.Trees;
using System.Linq;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.SetupFixtureTreeBuilderSpecs
{
    [Subject(typeof(SetupFixtureTreeBuilder))]
    public class When_tree_is_built_from_root_fixture_with_two_children : SetupFixtureTreeBuilderSpecification
    {
        private Establish context = () =>
        {
            rootFixture = AddFixtureFor<RootSetupFixture>();
            firstFixture = AddFixtureFor<NamespaceSetupFixture>();
            secondFixture = AddFixtureFor<AnotherNamespaceSetupFixture>();
        };

        Because of = () =>
            Tree = Subject.BuildFrom(AssemblyPath);

        It should_contain_root_fixture = () => Tree.Children.Values().ShouldContainOnly(rootFixture);

        It should_fill_root_fixture_children_with_child_fixtures = () => Tree.Children.First().Children.Values().ShouldContainOnly(firstFixture, secondFixture);

        private static ISetupFixture rootFixture;
        private static ISetupFixture firstFixture;
        private static ISetupFixture secondFixture;
    }
}