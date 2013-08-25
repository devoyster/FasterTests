namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public static class SetupFixtureExtensions
    {
        public static bool IsSetupSucceeded(this ISetupFixture setupFixture)
        {
            return setupFixture.State == SetupFixtureState.SetupSucceeded;
        }
    }
}