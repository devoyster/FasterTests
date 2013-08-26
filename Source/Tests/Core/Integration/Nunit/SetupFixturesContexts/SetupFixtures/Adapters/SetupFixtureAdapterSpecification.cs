using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.Adapters
{
    public abstract class SetupFixtureAdapterSpecification<TSetupFixture> : WithSubject<SetupFixtureAdapter> where TSetupFixture : class
    {
        Establish context = () =>
            Configure(r => r.For<Type>().Use(typeof(TSetupFixture)));
    }
}