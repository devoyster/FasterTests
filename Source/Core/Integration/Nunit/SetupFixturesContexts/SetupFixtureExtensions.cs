namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts
{
    public static class SetupFixtureExtensions
    {
        public static bool IsSetupFailed(this ISetupFixture setupFixture)
        {
            return setupFixture.State == SetupFixtureState.SetupFailed;
        }

        public static bool IsSetupExecuted(this ISetupFixture setupFixture)
        {
            return setupFixture.State != SetupFixtureState.NoSetupExecuted;
        }
    }
}