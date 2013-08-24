namespace FasterTests.Core.Integration.Nunit
{
    public static class SetupFixtureExtensions
    {
        public static bool IsSetupSucceeded(this ISetupFixture setupFixture)
        {
            return setupFixture.State == SetupFixtureState.SetupSucceeded;
        }
    }
}