using FasterTests.Helpers.Trees;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public interface ISetupFixtureTreeBuilder
    {
        ITree<ISetupFixture> BuildFrom(string assemblyPath);
    }
}