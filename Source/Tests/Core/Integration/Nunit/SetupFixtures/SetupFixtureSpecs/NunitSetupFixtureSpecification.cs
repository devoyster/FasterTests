using FasterTests.Core.Integration.Nunit.SetupFixtures;
using Machine.Specifications;
using Machine.Specifications.Annotations;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixtures.SetupFixtureSpecs
{
    public abstract class NunitSetupFixtureSpecification<TSetupFixture> where TSetupFixture : class
    {
        [UsedImplicitly]
        Establish context = () =>
            Subject = new SetupFixture(typeof(TSetupFixture));

        protected static SetupFixture Subject { get; private set; }
    }
}