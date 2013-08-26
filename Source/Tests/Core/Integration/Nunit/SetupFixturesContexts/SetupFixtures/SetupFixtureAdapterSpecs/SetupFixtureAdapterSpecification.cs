using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using Machine.Fakes;
using Machine.Specifications;
using NUnit.Core;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureAdapterSpecs
{
    public abstract class SetupFixtureAdapterSpecification<TSetupFixture> : WithSubject<SetupFixtureAdapter> where TSetupFixture : class
    {
        Establish context = () =>
            Configure(r => r.For<Type>().Use(typeof(TSetupFixture)));
    }
}