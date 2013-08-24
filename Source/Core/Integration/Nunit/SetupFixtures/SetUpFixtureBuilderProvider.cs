using NUnit.Core.Builders;

namespace FasterTests.Core.Integration.Nunit.SetupFixtures
{
    public static class SetUpFixtureBuilderProvider
    {
        public static SetUpFixtureBuilder Instance = new SetUpFixtureBuilder();
    }
}