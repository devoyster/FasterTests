using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    public abstract class SetupFixtureSpecification<TSetupFixture> : WithSubject<SetupFixture> where TSetupFixture : class
    {
        Establish context = () =>
            Configure(r => r.For<Type>().Use(typeof(TSetupFixture)));
    }
}