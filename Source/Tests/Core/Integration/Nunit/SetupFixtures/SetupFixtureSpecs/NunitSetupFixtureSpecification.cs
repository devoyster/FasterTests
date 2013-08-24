using System;
using FasterTests.Core.Integration.Nunit.SetupFixtures;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixtures.SetupFixtureSpecs
{
    public abstract class NunitSetupFixtureSpecification<TSetupFixture> : WithSubject<SetupFixture> where TSetupFixture : class
    {
        Establish context = () =>
            Configure(r => r.For<Type>().Use(typeof(TSetupFixture)));
    }
}