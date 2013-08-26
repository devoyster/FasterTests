using System.Collections.Generic;
using System.Linq;
using FasterTests.Helpers.Collections;
using FasterTests.Helpers.Trees;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees
{
    public class SetupFixtureTreeBuilder : ISetupFixtureTreeBuilder
    {
        private readonly ISetupFixtureFactory _setupFixtureFactory;

        public SetupFixtureTreeBuilder(ISetupFixtureFactory setupFixtureFactory)
        {
            _setupFixtureFactory = setupFixtureFactory;
        }

        public ITree<ISetupFixture> BuildFrom(string assemblyPath)
        {
            var root = Tree.Root(RootSetupFixtureStub.Instance);

            var allFixtures = CreateAllFixtures(assemblyPath);
            AddFixturesToTree(root, allFixtures.ToHeadAndTail());

            return root;
        }

        private IEnumerable<ISetupFixture> CreateAllFixtures(string assemblyPath)
        {
            return _setupFixtureFactory.CreateAllFrom(assemblyPath)
                    .GroupBy(f => f.Type.Namespace)
                    .Select(g => g.First());
        }

        private void AddFixturesToTree(ITree<ISetupFixture> tree, HeadAndTail<ISetupFixture> headAndTail)
        {
            if (!headAndTail.Head.IsSome)
            {
                return;
            }

            var head = headAndTail.Head.Value;
            if (tree.Value.IsRequiredFor(head))
            {
                AddFixturesToTree(tree.Add(head), headAndTail.Tail.ToHeadAndTail());
            }
            else
            {
                AddFixturesToTree(tree.Parent, headAndTail);
            }
        }
    }
}