using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;
using FasterTests.Helpers.Trees;
using System.Linq;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.SetupFixtureTreeBuilderSpecs
{
    [Subject(typeof(SetupFixtureTreeBuilder))]
    public class When_tree_is_built_from_global_fixture_with_one_children : SetupFixtureTreeBuilderSpecification
    {
        private Establish context = () =>
        {
            globalFixture = AddFixtureFor<GlobalSetupFixture>();
            rootFixture = AddFixtureFor<RootSetupFixture>();
        };

        Because of = () =>
            Tree = Subject.BuildFrom(AssemblyPath);

        It should_contain_global_fixtures = () => Tree.Children.Values().ShouldContainOnly(globalFixture);

        It should_fill_global_fixture_children_with_child_fixture = () => Tree.Children.First().Children.Values().ShouldContainOnly(rootFixture);

        private static ISetupFixture globalFixture;
        private static ISetupFixture rootFixture;
    }
}