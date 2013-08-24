using FasterTests.Core.Integration.Nunit.SetupFixtures;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixtures.SetupFixtureSpecs
{
    public abstract class NunitSetupFixtureSpecification<TSetupFixture> : WithFakes where TSetupFixture : class
    {
        Establish context = () =>
            Subject = new SetupFixture(typeof(TSetupFixture));

        protected static SetupFixture Subject { get; private set; }
    }
}