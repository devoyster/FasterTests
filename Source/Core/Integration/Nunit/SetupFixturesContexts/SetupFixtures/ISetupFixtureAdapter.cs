namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public interface ISetupFixtureAdapter
    {
        bool Setup();

        void Teardown();
    }
}